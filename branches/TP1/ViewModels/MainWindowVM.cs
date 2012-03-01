using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using TP1.Utils;
using System;
using System.Reflection;
using System.Diagnostics;

namespace TP1.ViewModels
{
    class MainWindowVM : BindableObject
    {
        public MainWindowVM()
        {
            Operators = new StringCollection();
            Operators.Add("+");
            Operators.Add("-");
            Operators.Add("*");
            Operators.Add("/");
            commandEval = new RelayCommand(param => this.evaluate((int)param));
            ValueA = -10;
            ValueB = 15;
            Result = 35;
        }

        public StringCollection Operators { get; private set; }
        public int ValueA { get; set; }
        public int ValueB { get; set; }
        public int Result { get; set; }

        public ICommand commandEval { get; private set; }
        public void evaluate(int op)
        {
            switch (op)
            {
                case 0:
                    Result = ValueA + ValueB;
                    break;
                case 1:
                    Result = ValueA - ValueB;
                    break;
                case 2:
                    Result = ValueA * ValueB;
                    break;
                case 3:
                    Result = ValueA / ValueB;
                    break;
            }
            RaisePropertyChange("Result");
        }
    }
}
