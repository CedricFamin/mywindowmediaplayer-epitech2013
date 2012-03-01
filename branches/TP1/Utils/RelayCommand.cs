using System;
using System.Windows.Input;

namespace TP1.Utils
{
    /// <summary>
    /// A basic command class using lambda expression
    /// </summary>
    class RelayCommand : ICommand
    {

        private Action<object> exec;
        private Func<object, bool> canExecute;

        private RelayCommand() { }

        public RelayCommand(Action<object> ex)
        {
            this.exec = ex;
            this.canExecute = parem => true;
        }

        public RelayCommand(Action<object> ex, Func<object, bool> canExec)
        {
            this.exec = ex;
            this.canExecute = canExec;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            this.exec(parameter);
        }
    }
}
