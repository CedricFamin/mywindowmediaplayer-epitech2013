using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using MWMP.Models;
using MWMP.Utils;
using MWMP.ViewModels;
using System.Collections;
using MWMP.InjectionDepedency;
using MWMP;


namespace MusicPlayerViewModel
{

    public class  Module : GenericModule<MusicPlayerViewModel>
    {
        public Module() : base("MusicPlayerViewModel", true) { }
    }

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
        public IPlayList PlayList { get; private set; }
        public double DurationOnCurrentPlay
        {
            get
            {
                if (MediaElement.NaturalDuration.HasTimeSpan == false)
                    return 1;
                return MediaElement.NaturalDuration.TimeSpan.TotalSeconds;
            }
        }
        public double PosOnCurrentPlay
        {
            get
            {
                return MediaElement.Position.TotalSeconds;
            }
            set
            {
                this.MediaElement.Position = new TimeSpan(0, 0, (int)value);
            }
        }
        public int CurrentMedia { get; set; }
        #endregion

        #region Command
        public ICommand Stop { get; protected set; }
        public ICommand Next { get; protected set; }
        public ICommand Pause { get; protected set; }
        public ICommand Play { get; protected set; }
        public ICommand Open { get; protected set; }
        public ICommand AddMediaToPlayList { get; protected set; }
        #endregion

        #region Ctor
        public MusicPlayerViewModel()
        {
            PlayList = ModuleManager.GetInstanceOf<IPlayList>("PlayList");
            PlayList.Title = "Current PlayList";
            this._volume = 5;
            Play = new RelayCommand((param) => MediaElement.Play());
            Stop = new RelayCommand((param) => MediaElement.Stop());
            Pause = new RelayCommand((param) => MediaElement.Pause());
            //Next = new RelayCommand((param) => this.player.Next());
            AddMediaToPlayList = new RelayCommand((param) => this.AddMediaToPlayListCommand(param as IMedia));
            Open = new RelayCommand((param) => this.OpenCommand(param as IMedia));
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
                string format = "";
                if (MediaElement.NaturalDuration.TimeSpan.Hours > 0)
                    format += "hh':'";
                if (MediaElement.NaturalDuration.TimeSpan.Minutes > 0)
                    format += "mm':'";
                format += "ss";
                return MediaElement.Position.ToString(format) + "/" +
                       MediaElement.NaturalDuration.TimeSpan.ToString(format);
            }
        }
        #endregion

        #region private Methods
        private void clockTimer_Tick(object sender, EventArgs e)
        {
            RaisePropertyChange("Time");
            RaisePropertyChange("PosOnCurrentPlay");
            RaisePropertyChange("DurationOnCurrentPlay");
        }

        private void AddMediaToPlayListCommand(IMedia media)
        {
            if (media == null) return;
            PlayList.Add(media);
            RaisePropertyChange("PlayList");
        }

        private void OpenCommand(IMedia media)
        {
            if (media == null) return ;
            Stop.Execute(new Object[0]);
            PlayList.Clear();
            if (media == null) return;
            Source = media.Path;
            AddMediaToPlayList.Execute(media);
            RaisePropertyChange("Time");
            Play.Execute(new object[0]);
        }
        #endregion
    }
}
