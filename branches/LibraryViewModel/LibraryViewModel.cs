using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.ViewModels;
using MWMP.Models;
using MWMP;
using System.Collections;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.IO;
using MWMP.Models.DAL;
using System.Windows.Input;
using MWMP.Utils;

namespace LibraryViewModel
{
    public class LibraryViewModel : BindableObject, ILibrary
    {
        #region properties
        public ObservableCollection<IMusicMedia> MusicList { get; private set; }
        public ObservableCollection<IVideoMedia> VideoList { get; private set; }
        public ObservableCollection<IImageMedia> ImageList { get; private set; }
        public IMedia SelectedItem { get; set; }
        #endregion

        #region Ctor
        public LibraryViewModel()
        {
            MusicList = new ObservableCollection<IMusicMedia>();
            VideoList = new ObservableCollection<IVideoMedia>();
            ImageList = new ObservableCollection<IImageMedia>();
            PlayContextMenu = new RelayCommand((param) =>
                {
                    IMediaPlayer mp = ModuleManager.GetInstanceOf<IMediaPlayer>("MusicPlayerViewModel");
                    if (mp != null && SelectedItem != null)
                        mp.Open.Execute(SelectedItem);
                });
            EnableRaisePropertyChanged = false;
            IDAL DAL = ModuleManager.GetInstanceOf<IDAL>("XMLDAL");
            foreach (IMedia media in DAL.MediaList)
            {
                media.AddToLibrary(this);
            }
            EnableRaisePropertyChanged = true;
        }
        #endregion

        #region Dtor
        ~LibraryViewModel()
        {
            IDAL DAL = ModuleManager.GetInstanceOf<IDAL>("XMLDAL");

            foreach (IMusicMedia media in MusicList)
                DAL.Save(media, "MusicMedia");
            foreach (IVideoMedia media in VideoList)
                DAL.Save(media, "VideoMedia");
            foreach (IImageMedia media in ImageList)
                DAL.Save(media, "ImageMedia");
            DAL.Save();
        }
        #endregion

        #region Add method
        public void Add(IMusicMedia media)
        {
            MusicList.Add(media);
            RaisePropertyChange("MusicList");
        }

        public void Add(IVideoMedia media)
        {
            VideoList.Add(media);
            RaisePropertyChange("VideoList");
        }

        public void Add(IImageMedia media)
        {
            ImageList.Add(media);
            RaisePropertyChange("ImageList");
        }
        #endregion

        public ICommand PlayContextMenu { get; private set; }
    }
}
