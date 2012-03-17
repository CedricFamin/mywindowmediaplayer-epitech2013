using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MWMP.ViewModels
{
    public interface IMenuBar
    {
        ICommand Open { get; }
        ICommand Close { get; }
    }
}
