using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UTC2_Student.MVVM.Core
{
    public class AsyncRelayCommand : ICommand
    {
        private readonly Func<object, Task> _execute;
        private readonly Predicate<object>? _canExecute;


        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public AsyncRelayCommand(Func<object, Task> execute)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _execute = execute;
            _canExecute = null;
        }
        public AsyncRelayCommand(Func<object, Task> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        //public AsyncRelayCommand(Func<object, Task> execute) : this(execute, null)
        //{ }

        public virtual bool CanExecute(object? parameter)
        {
            //return _canExecute is null || _canExecute(parameter);
            return _canExecute == null ? true : _canExecute(parameter!);
        }

        public async void Execute(object? parameter)
        {
            try
            {
                await _execute.Invoke(parameter!);
            }
            catch (Exception ex)
            {
                if (ex.Message != "A task was canceled.")
                    throw;
            }
        }
    }
}
