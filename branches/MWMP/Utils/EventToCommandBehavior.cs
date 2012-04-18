using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows;
using System.Windows.Input;

namespace MWMP.Utils
{
    public class EventToCommandBehavior : TriggerAction<DependencyObject>
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(EventToCommandBehavior));
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(EventToCommandBehavior));

        #region Properties
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public ICommand Command
        {
            get { return GetValue(CommandProperty) as ICommand; }
            set { SetValue(CommandParameterProperty, value); }
        }
        #endregion

        protected override void Invoke(object parameter)
        {
            if (this.Command == null)
                return;

            if (this.Command.CanExecute(parameter))
            {
                object[] parameters = new object[]
                {
                    parameter,
                    AssociatedObject,
                    CommandParameter
                };
                Command.Execute(parameters);
            }
        }
    }
}
