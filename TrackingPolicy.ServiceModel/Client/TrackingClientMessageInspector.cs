// Source: https://github.com/ajdotnet/wcf-policy
using AJ.Common;
using System.Diagnostics;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace TrackingPolicy.ServiceModel.Client
{
    /// <summary>
    /// Message inspector on the client.
    /// </summary>
    internal class TrackingClientMessageInspector : IClientMessageInspector
    {
        ITrackingInformation _providedTrackingInformation;  // only in case the application code has provided them
        private string _thisClient;     // text identifying this client (could be the URL of the called service if this is a nested call, or the module name) 
        private string _calledService;  // text identifying the called service (typically the URL)

        public TrackingClientMessageInspector(ServiceEndpoint endpoint)
        {
            CalculateInformation(endpoint);
        }

        public TrackingClientMessageInspector(ServiceEndpoint endpoint, ITrackingInformation providedTrackingInformation)
        {
            _providedTrackingInformation = providedTrackingInformation;
            CalculateInformation(endpoint);
        }

        private void CalculateInformation(ServiceEndpoint endpoint)
        {
            if (OperationContext.Current != null)
                // in case this is a service calling out to another service, we are already in a service call.
                _thisClient = "[" + Path.GetFileName(OperationContext.Current.EndpointDispatcher.EndpointAddress.Uri.ToString()) + "]";
            else
                _thisClient = "[" + Path.GetFileName(Process.GetCurrentProcess().MainModule.ModuleName) + "]";

            _calledService = Path.GetFileName(endpoint.Address.Uri.ToString());
        }

        private ITrackingInformation GetTrackingInformation()
        {
            // either the application code has provided headers
            // or we maintain the header values in a temporary store.
            var tracking = _providedTrackingInformation ?? new TrackingInformation();
            return tracking;
        }

        object IClientMessageInspector.BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            Guard.AssertNotNull(request, "request");

            // get headers
            var tracking = GetTrackingInformation();

            // add send information 
            tracking.OutgoingHeaders.TrackingEntries.Add(new TrackingEntry(_thisClient, "Send Request to " + _calledService));

            // tracking entries header
            var trackingEntriesHeader = new TrackingEntriesHeader(tracking.OutgoingHeaders.TrackingEntries);
            request.Headers.Add(trackingEntriesHeader);

            // pass headers via correlation state
            return tracking;
        }

        void IClientMessageInspector.AfterReceiveReply(ref Message reply, object correlationState)
        {
            Guard.AssertNotNull(reply, "reply");

            var tracking = (ITrackingInformation)correlationState;

            // tracking entries header
            var trackingEntriesHeader = TrackingEntriesHeader.ReadHeader(reply.Headers);
            if (trackingEntriesHeader != null)
                tracking.IncomingHeaders.TrackingEntries.AddRange(trackingEntriesHeader.TrackingEntries);

            // add receive information 
            tracking.IncomingHeaders.TrackingEntries.Add(new TrackingEntry(_thisClient, "Received Reply from " + _calledService));
        }
    }
}
