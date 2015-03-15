// Source: https://github.com/ajdotnet/wcf-policy
using System.ServiceModel;
using System.Threading;
using System.Xml;
using TrackingPolicy.ServiceModel;

namespace TrackingPolicy.TestServer.Services
{

    /// <summary>
    /// Service using the service behavior via configuration.
    /// </summary>
    public class TestService2 : ITestService
    {
        static int _sleep = 500;

        public string Ping(string message)
        {
            message += " | TestService";
            DoLengthyProcessing();
            return message;
        }

        static void DoLengthyProcessing()
        {
            var tracking = OperationContext.Current.Extensions.Find<ITrackingInformation>();

            tracking.OutgoingHeaders.TrackingEntries.Add(new TrackingEntry(typeof(TestService).Name, "Lengthy Processing: " + _sleep + " ms."));
            Thread.Sleep(_sleep);
            tracking.OutgoingHeaders.TrackingEntries.Add(new TrackingEntry(typeof(TestService).Name, "Lengthy Processing done."));
        }
    }
}
