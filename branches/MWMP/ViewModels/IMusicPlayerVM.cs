using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Utils;
using System.Windows.Controls;

namespace MWMP.ViewModels
{
    public interface IMusicPlayerVM
    {
        MediaElement MediaElement { set; get; }
        string Source { set; get; }
        double Volume { set; get; }
        string Time { get; }
        MediaState LoadedBehavior { set; get; }
        bool CanCommandExecute { set; get; }
        RelayCommand Stop { get; }
        RelayCommand Next { get; }
        RelayCommand Pause { get; }
        RelayCommand Play { get; }
        RelayCommand Open { get; }
    }
}
