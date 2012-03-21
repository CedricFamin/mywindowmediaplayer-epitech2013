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
    public class MusicPlayerViewModel : BindableObject, IMediaPlayer
    {
        #region Fields
        private string _source;
        private double _volume;
        private DispatcherTimer clockTimer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
        #endregion

        #region Properties
        public MediaElement MediaElement { set; get; }
        public MediaState LoadedBehavior { set; get; }
        public bool CanCommandExecute { set; get; }
        #endregion

        #region Command
        public RelayCommand Stop { get; protected set; }
        public RelayCommand Next { get; protected set; }
        public RelayCommand Pause { get; protected set; }
        public RelayCommand Play { get; protected set; }
        public RelayCommand Open { get; protected set; }
        #endregion

        #region Ctor
        public MusicPlayerViewModel()
        {
            this._volume = 5;
            Play = new RelayCommand((param) => MediaElement.Play());
            Stop = new RelayCommand((param) => MediaElement.Stop());
            Pause = new RelayCommand((param) => MediaElement.Pause());
            //Next = new RelayCommand((param) => this.player.Next());
            Open = new RelayCommand((param) =>
            {
                Source = param as string;
                RaisePropertyChange("Time");
                Play.Execute(new object[0]);
            });
            clockTimer.Tick += clockTimer_Tick;
            clockTimer.Start();
        }
        #endregion

        #region Methods
        public string Source 
        { 
            set
            {
                this._source = value;
                RaisePropertyChange("Source");
                RaisePropertyChange("Time");
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
                       MediaElement.NaturalDuration.TimeSpan.Seconds.ToString();
            }
        }
        #endregion

        #region private Methods
        private void clockTimer_Tick(object sender, EventArgs e)
        {
            RaisePropertyChange("Time");
        }
        #endregion
    }
}
