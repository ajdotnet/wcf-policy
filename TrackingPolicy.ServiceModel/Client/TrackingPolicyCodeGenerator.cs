// Source: https://github.com/ajdotnet/wcf-policy
using AJ.Common;
using System.CodeDom;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace TrackingPolicy.ServiceModel.Client
{
    public sealed class TrackingPolicyCodeGenerator : IServiceContractGenerationExtension, IContractBehavior
    {
        void IServiceContractGenerationExtension.GenerateContract(ServiceContractGenerationContext context)
        {
            Guard.AssertNotNull(context, "context");

            // called during code generation for the service proxy on the client
            // using CodeDOM to add an attribute to the generated code
            var attr = new CodeAttributeDeclaration(new CodeTypeReference(typeof(TrackingContractBehaviorAttribute)));
            context.ContractType.CustomAttributes.Add(attr);
        }

        void IContractBehavior.AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        void IContractBehavior.ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        void IContractBehavior.ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
        }

        void IContractBehavior.Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
        }
    }
}
