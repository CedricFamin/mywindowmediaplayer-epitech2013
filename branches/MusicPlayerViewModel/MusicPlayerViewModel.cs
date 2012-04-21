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
using System.Windows;


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
        #endregion

        #region Properties
        public int CurrentMedia { get; set; }
        public IPlayList PlayList { get; private set; }
        public string Source
        {
            set
            {
                this._source = value;
                RaisePropertyChange("Source");
            }
            get { return this._source; }
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
        #endregion

        #region Command
        public ICommand ChangePist { get; private set; }
        public ICommand ChangeVolume { get; private set; }
        public ICommand Next { get; protected set; }
        public ICommand Previous { get; protected set; }
        public ICommand Open { get; protected set; }
        public ICommand AddMediaToPlayList { get; protected set; }
        #endregion

        #region Ctor
        public MusicPlayerViewModel()
        {
            PlayList = ModuleManager.GetInstanceOf<IPlayList>("PlayList");
            if (PlayList != null) PlayList.Title = "Current PlayList";
            this._volume = 0.5;
            ChangeVolume = new RelayCommand((param) => Volume = Convert.ToDouble(param));
            Next = new RelayCommand((param) => NextBody());
            Previous = new RelayCommand((param) => PreviousBody());
            ChangePist = new RelayCommand((param) => ChangePistBody(param as IMedia));
            AddMediaToPlayList = new RelayCommand((param) => AddMediaToPlayListCommand(param as IMedia));
            Open = new RelayCommand((param) => this.OpenCommand(param as IMedia));
        }

        private void ChangePistBody(IMedia media)
        {
            if (media == null)
                return;
            int index = PlayList.Collection.IndexOf(media);
            ChangeCurrentMedia(index);
        }
        #endregion

        #region Methods
        public void LoadedMediaFaild()
        {
            MessageBox.Show("Error during loading");
        }
        #endregion

        #region private Methods
        private void AddMediaToPlayListCommand(IMedia media)
        {
            if (media == null) return;
            PlayList.Add(media);
            RaisePropertyChange("PlayList");
            if (PlayList.Collection.Count == 1)
                ChangeCurrentMedia(0);
        }

        private void OpenCommand(IMedia media)
        {
            if (media == null) 
                return ;
            PlayList.Clear();
            if (media == null) return;
            AddMediaToPlayList.Execute(media); 
        }

        private void NextBody()
        {
            int indexOfCurrent = CurrentMedia + 1;
            if (PlayList.Collection.Count <= indexOfCurrent)
                indexOfCurrent = 0;
            ChangeCurrentMedia(indexOfCurrent);
        }
        private void PreviousBody()
        {
            int indexOfCurrent = CurrentMedia - 1;
            if (indexOfCurrent < 0)
                indexOfCurrent = PlayList.Collection.Count - 1;
            ChangeCurrentMedia(indexOfCurrent);
        }

        private void ChangeCurrentMedia(int index)
        {
            if (index <= PlayList.Collection.Count)
            {
                CurrentMedia = index;
                Source = PlayList.Collection[CurrentMedia].Path;
                RaisePropertyChange("CurrentMedia");
            }
        }
        #endregion
    }
}
