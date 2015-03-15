// Source: https://github.com/ajdotnet/wcf-policy
using AJ.Common;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace TrackingPolicy.ServiceModel.Server
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class TrackingBehaviorAttribute : Attribute, IServiceBehavior
    {
        void IServiceBehavior.AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            Guard.AssertNotNull(serviceDescription, "serviceDescription");
            Guard.AssertNotNull(serviceHostBase, "serviceHostBase");

            foreach (ChannelDispatcher cd in serviceHostBase.ChannelDispatchers)
            {
                foreach (EndpointDispatcher ep in cd.Endpoints)
                {
                    var inspector = new TrackingDispatchMessageInspector(ep);
                    ep.DispatchRuntime.MessageInspectors.Add(inspector);
                }
            }
        }

        void IServiceBehavior.Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            Guard.AssertNotNull(serviceDescription, "serviceDescription");
            Guard.AssertNotNull(serviceHostBase, "serviceHostBase");
            
            foreach (var endpoint in serviceDescription.Endpoints)
            {
                if (endpoint.Contract.ContractType != typeof(IMetadataExchange))
                    InsertPolicy(endpoint);
            }
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "serviceHostBase")]
        void IServiceBehavior_Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (var endpoint in serviceDescription.Endpoints)
            {
                // only custom bindings supported
                var binding = (CustomBinding)endpoint.Binding;
                binding.Elements.Insert(0, new TrackingPolicyExportExtension());
            }
        }

        private static void InsertPolicy(ServiceEndpoint endpoint)
        {
            if (endpoint.Binding.GetType() != typeof(CustomBinding))
                throw new InvalidOperationException("Only custom bindings support inserting the policy binding!");

            var binding = (CustomBinding)endpoint.Binding;
            var element = binding.Elements.OfType<TrackingPolicyExportExtension>().FirstOrDefault();
            if (element == null)
                binding.Elements.Insert(0, new TrackingPolicyExportExtension());
        }
    }
}
