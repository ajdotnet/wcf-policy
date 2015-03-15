// Source: https://github.com/ajdotnet/wcf-policy
using AJ.Common;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace TrackingPolicy.ServiceModel.Client
{
    /// <summary>
    /// Endpoint to explicitly enable the policy, and to access policy specific information
    /// <code>
    ///     TrackingHeaders outgoingHeaders = new TrackingHeaders();
    ///     var behavior = new TrackingEndpointBehavior(outgoingHeaders);
    ///     client.Endpoint.EndpointBehaviors.Add(behavior);
    /// </code>
    /// </summary>
    public sealed class TrackingEndpointBehavior : IEndpointBehavior, ITrackingInformation
    {
        public TrackingHeaders IncomingHeaders { get; private set; }
        public TrackingHeaders OutgoingHeaders { get; private set; }

        public TrackingEndpointBehavior()
        {
            this.OutgoingHeaders = new TrackingHeaders();
            this.IncomingHeaders = new TrackingHeaders();
        }

        public TrackingEndpointBehavior(TrackingHeaders outgoingHeaders)
        {
            this.OutgoingHeaders = outgoingHeaders ?? new TrackingHeaders();
            this.IncomingHeaders = new TrackingHeaders();
        }

        void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            Guard.AssertNotNull(endpoint, "endpoint");
            Guard.AssertNotNull(clientRuntime, "clientRuntime");

            // add the message inspector
            clientRuntime.MessageInspectors.Add(new TrackingClientMessageInspector(endpoint, this));
        }

        void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
        {
        }
    }
}
