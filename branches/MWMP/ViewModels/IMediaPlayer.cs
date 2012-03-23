using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Utils;
using System.Windows.Controls;
using System.Windows.Input;

namespace MWMP.ViewModels
{
    public interface IMediaPlayer
    {
        MediaElement MediaElement { set; get; }
        string Source { set; get; }
        double Volume { set; get; }
        string Time { get; }
        MediaState LoadedBehavior { set; get; }
        bool CanCommandExecute { set; get; }
		double DurationOnCurrentPlay  {get; }
        double PosOnCurrentPlay { get; set; }
        ICommand Stop { get; }
        ICommand Next { get; }
        ICommand Pause { get; }
        ICommand Play { get; }
        ICommand Open { get; }
        ICommand AddMediaToPlayList { get; }
    }
}
