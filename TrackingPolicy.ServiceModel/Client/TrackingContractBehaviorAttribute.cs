// Source: https://github.com/ajdotnet/wcf-policy
using AJ.Common;
using System;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace TrackingPolicy.ServiceModel.Client
{
    /// <summary>
    /// Attribute that is put on the generated contract interface.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class TrackingContractBehaviorAttribute : Attribute, IContractBehavior
    {
        void IContractBehavior.AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        void IContractBehavior.ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            Guard.AssertNotNull(contractDescription, "contractDescription");
            Guard.AssertNotNull(endpoint, "endpoint");
            Guard.AssertNotNull(clientRuntime, "clientRuntime");

            // EndpointBehavior might have been added by the client code and will install the MessageInspector!
            // If not, we install the MessageInspector here to provide the defaults for our policy.
            var endpointBehavior = endpoint.Behaviors.OfType<TrackingEndpointBehavior>().FirstOrDefault();
            if (endpointBehavior == null)
            {
                // Note: This MessageInspector is going to be shared among all respective instances, 
                // so don't use it to maintain any operation specific values!
                clientRuntime.MessageInspectors.Add(new TrackingClientMessageInspector(endpoint));
            }
        }

        void IContractBehavior.ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
        }

        void IContractBehavior.Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
        }
    }
}
