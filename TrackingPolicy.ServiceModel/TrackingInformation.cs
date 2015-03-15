// Source: https://github.com/ajdotnet/wcf-policy

namespace TrackingPolicy.ServiceModel
{
    /// <summary>
    /// Client: maintains header values temporarily, if no custom endpoint behavior has been supplied.
    /// Server: base class for the operation extension that surfaces the values to the service method
    /// </summary>
    internal class TrackingInformation : ITrackingInformation
    {
        public TrackingHeaders IncomingHeaders { get; private set; }
        public TrackingHeaders OutgoingHeaders { get; private set; }

        public TrackingInformation()
        {
            this.IncomingHeaders = new TrackingHeaders();
            this.OutgoingHeaders = new TrackingHeaders();
        }
    }
}
