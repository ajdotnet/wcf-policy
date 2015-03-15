// Source: https://github.com/ajdotnet/wcf-policy

namespace TrackingPolicy.ServiceModel
{
    /// <summary>
    /// The interface through which incomming and outgoing header information is made available to the user code. 
    /// </summary>
    public interface ITrackingInformation
    {
        TrackingHeaders IncomingHeaders { get; }
        TrackingHeaders OutgoingHeaders { get; }
    }
}
