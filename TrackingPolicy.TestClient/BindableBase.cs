// Source: https://github.com/ajdotnet/wcf-policy
using System.ComponentModel;

namespace TrackingPolicy.TestClient
{
    public class BindableBase: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged!=null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
