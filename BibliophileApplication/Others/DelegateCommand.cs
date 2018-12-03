using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BibliophileApplication.Utils
{
    public class DelegateCommand : ICommand
    {
        public Action<object> _execute;
        public Predicate<object> _canexecute;

        public DelegateCommand(Action<object> execute) : this(execute, null) { }

        public DelegateCommand(Action<object> execute, Predicate<object> canexecute)
        {
            _execute = execute ?? throw new NullReferenceException("Execute delegate null refernce exception");
            _canexecute = canexecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canexecute == null ? true : _canexecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; } 
        }
    }
}
