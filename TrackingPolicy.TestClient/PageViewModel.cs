// Source: https://github.com/ajdotnet/wcf-policy
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Windows.Media;
using TrackingPolicy.ServiceModel;
using TrackingPolicy.ServiceModel.Client;

namespace TrackingPolicy.TestClient
{
    public class PageViewModel : BindableBase
    {
        #region binding properties

        private string _PingText;
        public string PingText
        {
            get { return _PingText; }
            set
            {
                if (_PingText == value)
                    return;
                _PingText = value;
                OnPropertyChanged("PingText");
            }
        }

        private string _ResultMessage;
        public string ResultMessage
        {
            get { return _ResultMessage; }
            set
            {
                if (_ResultMessage == value)
                    return;
                _ResultMessage = value;
                OnPropertyChanged("ResultMessage");
            }
        }

        private string _ResultState;
        public string ResultState
        {
            get { return _ResultState; }
            set
            {
                if (_ResultState == value)
                    return;
                _ResultState = value;
                OnPropertyChanged("ResultState");
            }
        }

        private Brush _ResultBrush = Brushes.Gray;
        public Brush ResultBrush
        {
            get { return _ResultBrush; }
            set
            {
                if (_ResultBrush == value)
                    return;
                _ResultBrush = value;
                OnPropertyChanged("ResultBrush");
            }
        }

        public DelegateCommand CommandPing { get; set; }

        #endregion

        #region ctors

        [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "TrackingPolicy.TestClient.PageViewModel.SetResult(System.String,System.Windows.Media.Brush,System.String)")]
        public PageViewModel()
        {
            CommandPing = new DelegateCommand(OnCommandPing);

            PingText = "Some message...";

            SetResult("No state yet...", Brushes.Gray, "Result will appear here...");
        }

        #endregion

        #region commands

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "TrackingPolicy.TestClient.PageViewModel.SetResultPending(System.String)")]
        private void OnCommandPing(object parameter)
        {
            SetResultPending("Calling Ping...");

            try
            {
                using (var client = new TestServiceReference.TestServiceClient())
                {
                    var behavior = new TrackingEndpointBehavior();
                    behavior.OutgoingHeaders.TrackingEntries.Add(new TrackingEntry("my code", "just for fun"));
                    client.Endpoint.Behaviors.Add(behavior);

                    var request = this.PingText;
                    var response = client.Ping(request);

                    var msg = ToStrings("BODY", response)
                        .Concat(EmptyLine())
                        .Concat(ToStrings(behavior.IncomingHeaders.TrackingEntries));
                    SetResult(msg);
                }
            }
            catch (Exception ex)
            {
                SetResult(ex);
            }
        }

        #endregion

        #region set result...

        void SetResult(string state, Brush brush, string message)
        {
            ResultState = state;
            ResultBrush = brush;
            ResultMessage = message;
        }

        void SetResult(string message)
        {
            SetResult("Done.", Brushes.Green, message);
        }

        void SetResult(IEnumerable<string> msg)
        {
            SetResult(string.Join(Environment.NewLine, msg));
        }

        void SetResultPending(string message)
        {
            SetResult("Operation in progress...", Brushes.Blue, message);
        }

        private void SetResult(Exception ex)
        {
            SetResult("EXCEPTION: " + ex.GetType().FullName, Brushes.Red, ex.ToString());
        }

        private IEnumerable<string> EmptyLine()
        {
            yield return "";
        }

        private IEnumerable<string> ToStrings(string name, string value)
        {
            yield return name + ": " + (value ?? string.Empty);
        }

        private static IEnumerable<string> ToStrings(IEnumerable<TrackingEntry> trackingEntries)
        {
            var start = trackingEntries.First().Timestamp;
            var translated = trackingEntries.Select(e => ToString(e, start));
            return translated;
        }

        private static string ToString(TrackingEntry trackingEntry, DateTime start)
        {
            var time = trackingEntry.Timestamp - start;
            return string.Format(CultureInfo.InvariantCulture, "{0} {1} - {2}",
                time.ToString("s\\.ffff", CultureInfo.InvariantCulture), trackingEntry.Source, trackingEntry.Reason);
        }

        #endregion
    }
}
