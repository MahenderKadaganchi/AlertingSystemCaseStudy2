using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DelegateCommandLib
{
    public class DelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Func<object, bool> _canExecuteMethodAddress;
        private Action<object> _executeMethodAddress;

        public DelegateCommand(Action<object> executeMethodAddress, Func<object, bool> canExecuteMethodAddress)
        {
            this._executeMethodAddress = executeMethodAddress;
            this._canExecuteMethodAddress = canExecuteMethodAddress;
        }
        public bool CanExecute(object parameter)
        {
            return this._canExecuteMethodAddress.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            this._executeMethodAddress.Invoke(parameter);
        }
    }
}
