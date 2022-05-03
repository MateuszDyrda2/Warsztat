using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Warsztat.Commands
{
    public class RelayCommand : ICommand
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute) : this(execute, null) { }

        public RelayCommand(Action<object>execute, Predicate<object> canExecute)
        {
            _execute = execute ?? throw new ArgumentException("execute");
            _canExecute = canExecute;
        }

        public bool CanExecute(object parametr)
        {
            return _canExecute == null || _canExecute(parametr);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parametr)
        {
            _execute(parametr);
        }
    }
}
