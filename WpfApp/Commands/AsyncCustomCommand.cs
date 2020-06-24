using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp.Commands
{
    public class AsyncCustomCommand : ICommand
    {
        public AsyncCustomCommand(Func<object, Task> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        public AsyncCustomCommand(Func<object, Task> execute)
        {
            _execute = execute;
        }

        private Func<object, Task> _execute;
        private Func<object, bool> _canExecute;
        public bool IsExecuting { get; private set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (IsExecuting)
                return false;
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public async void Execute(object parameter)
        {
            IsExecuting = true;
            await _execute?.Invoke(parameter);
            IsExecuting = false;
        }
    }
}
