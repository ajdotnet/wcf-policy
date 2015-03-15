// Source: https://github.com/ajdotnet/wcf-policy
using System.Collections.Generic;

namespace TrackingPolicy.ServiceModel
{
    /// <summary>
    ///   <para>
    /// This class encaspulates all policy specific headers comming in or going out.
    ///   </para>
    ///   <para>
    /// Technically we have only one property, but there may be more; thus an encapsulating class makes sense.
    /// If Incomming and outgoing headers differ considerably, separate classes would make more sense.
    ///   </para>
    /// </summary>
    public class TrackingHeaders
    {
        public List<TrackingEntry> TrackingEntries { get; private set; }

        public TrackingHeaders()
        {
            TrackingEntries = new List<TrackingEntry>();
        }
    }
}
