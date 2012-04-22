using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Models;
using System.Windows.Input;

namespace MWMP.ViewModels
{
    public interface IPlayListLibrary : ILibrary<IPlayList>
    {
        ICommand ChangeSelectedItem { get; }
    }
}
