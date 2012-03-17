using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.ViewModels;
using System.Windows.Input;
using MWMP.Utils;
using MWMP;
using MWMP.Models;
using System.Windows;
using Microsoft.Win32;
using MediaInfoLib;


namespace MenuBar
{
    public class MenuBar : IMenuBar
    {
        private static bool KindOfVideo(IInfoMedia media)
        {
            string format = media.Get("InternetMediaType");
            return format.Contains("video");
        }

        private static bool KindOfMusic(IInfoMedia media)
        {
            string format = media.Get("InternetMediaType");
            return format.Contains("audio");
        }

        private static bool KindOfImage(IInfoMedia media)
        {
            string format = media.Get("InternetMediaType");
            return format.Contains("image");
        }

        private RelayCommand _open { get; set; }
        private RelayCommand _close { get; set; }
        private ILibrary _libraryModel { get; set; }
        private IMediaPlayer _mediaPlayer { get; set; }

        public MenuBar()
        {
            _libraryModel = ModuleManager.GetInstanceOf<ILibrary>("LibraryViewModel");
            _mediaPlayer = ModuleManager.GetInstanceOf<IMediaPlayer>("MusicPlayerViewModel");
            Open = new RelayCommand((param) => this.OpenCommand());
            Close = new RelayCommand((param) => Application.Current.Shutdown());
        }

        public ICommand Open { get; private set; }
        public ICommand Close { get; private set; }

        private void OpenCommand()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            IMedia media = null;

            ofd.Title = "Open file";
            if (ofd.ShowDialog() != true)
                return;
            string path = ofd.FileName;
            if (path == null)
                return;
            _mediaPlayer.Source = path;
            _mediaPlayer.Play.Execute(new object());
            IInfoMedia mediaInfo = ModuleManager.GetInstanceOf<IInfoMedia>("InfoMedia");
            if (mediaInfo.Open(path))
            {
                //if (KindOfImage(media)) mmedia = ModuleManager.GetInstanceOf<IMusicMedia>("MusicMedia");
                if (KindOfMusic(mediaInfo)) media = ModuleManager.GetInstanceOf<IMusicMedia>("MusicMedia");
                if (KindOfVideo(mediaInfo)) media = ModuleManager.GetInstanceOf<IVideoMedia>("VideoMedia");
                if (media != null)
                {
                    media.SetInfo(mediaInfo);
                    media.AddToLibrary(_libraryModel);
                }
            }
        }
    }
}
