// Source: https://github.com/ajdotnet/wcf-policy
using System;
using System.Windows.Input;

namespace TrackingPolicy.TestClient
{
    public class DelegateCommand: ICommand
    {
        private Action<object> _action;

        public DelegateCommand(Action<object> action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return _action != null;
        }

        public event EventHandler CanExecuteChanged;

        public void OnCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }
    }
}
