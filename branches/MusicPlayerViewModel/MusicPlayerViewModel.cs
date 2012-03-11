using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Utils;
using MWMP.Models;
using MWMP.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.ComponentModel;
using MWMP;


namespace MusicPlayerViewModel
{
    public class MusicPlayerViewModel : BindableObject, IMusicPlayerVM
    {
        private string _source;
        private double _volume;

        public string Source 
        { 
            set
            {
                this._source = value;
                RaisePropertyChange("Source");
            }
            get {return this._source; }
        }

        public double Volume
        {
            set
            {
                this._volume = value;
                RaisePropertyChange("Volume");
            }
            get { return this._volume; }
        }

        public string Time
        {
            get
            {
                if (MediaElement.NaturalDuration.HasTimeSpan == false)
                    return "0:0:0/0:0:0";
                return MediaElement.Position.Hours.ToString() + ":" + 
                       MediaElement.Position.Minutes.ToString() + ":" + 
                       MediaElement.Position.Seconds.ToString() + "/" +
                       MediaElement.NaturalDuration.TimeSpan.Hours.ToString() + ":" +
                       MediaElement.NaturalDuration.TimeSpan.Minutes.ToString() + ":" +
                       MediaElement.NaturalDuration.TimeSpan.Seconds.ToString() + ":";
            }
        }

        public MediaElement MediaElement { set; get; }
        public MediaState LoadedBehavior { set; get; }
        public bool CanCommandExecute { set; get; }
        private DispatcherTimer clockTimer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
     
        public MusicPlayerViewModel()
        {
            Play = new RelayCommand((param) => MediaElement.Play());
            Stop = new RelayCommand((param) => MediaElement.Stop());
            Pause = new RelayCommand((param) => MediaElement.Pause());
            //Next = new RelayCommand((param) => this.player.Next());
            Open = new RelayCommand((param) =>
                {
                    Source = param as string;
                    ILibraryViewModel lvm = ModuleManager.getInstanceOf<ILibraryViewModel>("LibraryViewModel");
                    if (lvm != null)
                    {
                        IMusicMedia media = ModuleManager.getInstanceOf<IMusicMedia>("MusicMedia");
                        if (media != null)
                        {
                            media.Open(Source);
                            lvm.Add(media);
                        }
                    }
                    RaisePropertyChange("Time");
                });
            clockTimer.Tick += clockTimer_Tick;
            clockTimer.Start();
        }
        public RelayCommand Stop { get; protected set; }
        public RelayCommand Next { get; protected set; }
        public RelayCommand Pause { get; protected set; }
        public RelayCommand Play { get; protected set; }
        public RelayCommand Open { get; protected set; }
        private void clockTimer_Tick(object sender, EventArgs e)
        {
            RaisePropertyChange("Time");
        }
    }
}
