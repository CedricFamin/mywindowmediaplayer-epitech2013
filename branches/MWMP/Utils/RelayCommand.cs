﻿using System;
using System.Windows.Input;

namespace MWMP.Utils
{
    /// <summary>
    /// A basic command class using lambda expression
    /// </summary>
    public class RelayCommand : ICommand
    {

        #region Fields
        private Action<object> exec;
        private Func<object, bool> canExecute;
        #endregion /// Fields

        #region Ctor
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
        #endregion /// Ctor

        #region Methods
        public bool CanExecute(object parameter)
        {
            return this.canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            this.exec(parameter);
        }
        #endregion /// Methods
    }
}
