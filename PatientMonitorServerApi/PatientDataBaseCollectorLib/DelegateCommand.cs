using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PatientDataBaseCollectorLib
{
    public class DelegateCommand: ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Action<object> _executeMethod;
        private readonly Func<object, bool> _canExecuteMethodAddress;

        public DelegateCommand(Action<object> executeMethodAddress, Func<object, bool> canExecuteMethodAddress)
        {
            this._executeMethod = executeMethodAddress;
            this._canExecuteMethodAddress = canExecuteMethodAddress;

        }


        public bool CanExecute(object parameter)
        {
            return this._canExecuteMethodAddress.Invoke(parameter);
        }



        public void Execute(object parameter)
        {
            this._executeMethod.Invoke(parameter);
        }
        
    }
}

