using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Utils;
using MWMP.Models;

namespace MWMP.ViewModels
{
    class MusicPlayerViewModel : APlayer
    {
        public MusicPlayerViewModel()
        {
            MusicPlayer.Initialize();
            pathOfCurrentFile = "";
            Play = new RelayCommand((param) =>
            {
                if (!string.IsNullOrEmpty(pathOfCurrentFile))
                    MusicPlayer.PlayMusic(pathOfCurrentFile);
            });
            Stop = new RelayCommand((param) => MusicPlayer.StopMusic());
            Open = new RelayCommand((param) => pathOfCurrentFile = (string)param);
        }
        public string pathOfCurrentFile { get; private set; }
        public override RelayCommand Stop { get; protected set; }
        public override RelayCommand Play { get; protected set; }
        public override RelayCommand Speaker { get; protected set; }
        public override RelayCommand goTo { get; protected set; }
        public override RelayCommand Open { get; protected set; }
    }
}
