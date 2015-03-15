// Source: https://github.com/ajdotnet/wcf-policy
using AJ.Common;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace TrackingPolicy.ServiceModel.Server
{
    /// <summary>
    /// Message inspector on the service.
    /// </summary>
    internal class TrackingDispatchMessageInspector : IDispatchMessageInspector
    {
        string _thisService; // text identifying this service (the URL) 

        public TrackingDispatchMessageInspector(EndpointDispatcher ep)
        {
            _thisService = "[" + Path.GetFileName(ep.EndpointAddress.ToString()) + "]";
        }

        private static ITrackingInformation GetTrackingInformation()
        {
            // maintain values and surfaces them via operationcontext to the service method
            var extension = new TrackingOperationContextExtension();
            OperationContext.Current.Extensions.Add(extension);
            return extension;
        }

        object IDispatchMessageInspector.AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            Guard.AssertNotNull(request, "request");

            // install operation extension
            var tracking = GetTrackingInformation();

            // tracking entries header
            var trackingEntriesHeader = TrackingEntriesHeader.ReadHeader(request.Headers);
            if (trackingEntriesHeader != null)
                tracking.IncomingHeaders.TrackingEntries.AddRange(trackingEntriesHeader.TrackingEntries);

            // add receive information 
            tracking.IncomingHeaders.TrackingEntries.Add(new TrackingEntry(_thisService, "Received Request"));

            // and pass on to outgoing headers
            tracking.OutgoingHeaders.TrackingEntries.AddRange(tracking.IncomingHeaders.TrackingEntries);

            // pass headers via correlation state
            return tracking;
        }

        void IDispatchMessageInspector.BeforeSendReply(ref Message reply, object correlationState)
        {
            Guard.AssertNotNull(reply, "reply");
            
            var tracking = (ITrackingInformation)correlationState;

            // add send information 
            tracking.OutgoingHeaders.TrackingEntries.Add(new TrackingEntry(_thisService, "Send Reply"));

            // tracking entries header
            var trackingEntriesHeader = new TrackingEntriesHeader(tracking.OutgoingHeaders.TrackingEntries);
            reply.Headers.Add(trackingEntriesHeader);
        }
    }
}
