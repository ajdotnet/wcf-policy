// Source: https://github.com/ajdotnet/wcf-policy
using System;
using System.Diagnostics;

namespace TrackingPolicy.ServiceModel
{
    /// <summary>
    /// single tracking entry.
    /// </summary>
    [DebuggerDisplay("TrackingEntry: {Timestamp} {Source} - {Reason}")]
    public class TrackingEntry
    {
        public DateTime Timestamp { get; private set; }
        public string Source { get; private set; }
        public string Reason { get; private set; }

        public TrackingEntry(string source, string reason)
        {
            this.Timestamp = DateTime.Now;
            this.Source = source;
            this.Reason = reason;
        }

        public TrackingEntry(string source, string reason, DateTime timestamp)
        {
            this.Timestamp = timestamp;
            this.Source = source;
            this.Reason = reason;
        }
    }
}
