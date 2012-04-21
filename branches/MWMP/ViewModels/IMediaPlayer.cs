using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Utils;
using System.Windows.Controls;
using System.Windows.Input;
using MWMP.Models;
using System.Windows;

namespace MWMP.ViewModels
{
    public interface IMediaPlayer
    {
        string Source { set; get; }
        double Volume { set; get; }
        int CurrentMedia { get; set; }
        IPlayList PlayList { get;}

        void LoadedMediaFaild();

        ICommand ChangePist { get; }
        ICommand Next { get; }
        ICommand Previous { get; }
        ICommand Open { get; }
        ICommand ChangeVolume { get; }
        ICommand AddMediaToPlayList { get; }
    }
}
