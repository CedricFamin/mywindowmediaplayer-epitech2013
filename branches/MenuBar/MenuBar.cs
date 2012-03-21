using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Win32;
using MWMP;
using MWMP.Models;
using MWMP.Utils;
using MWMP.ViewModels;

namespace MenuBar
{
    public class MenuBar : IMenuBar
    {

        #region KindOf function
        private static bool KindOfVideo(IInfoMedia media)
        {
            string format = media.InternetMediaType;
            return format.Contains("video");
        }

        private static bool KindOfMusic(IInfoMedia media)
        {
            string format = media.InternetMediaType;
            return format.Contains("audio");
        }

        private static bool KindOfImage(IInfoMedia media)
        {
            string format = media.InternetMediaType;
            return format.Contains("image");
        }
        #endregion

        #region private property
        private RelayCommand _open { get; set; }
        private RelayCommand _close { get; set; }
        private ConcurrentStack<IMedia> _pendingMedia { get; set; }
        private DispatcherTimer _clockTimer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 100) };
        #endregion

        #region Property
        public ICommand Open { get; private set; }
        public ICommand Close { get; private set; }
        #endregion

        #region CTor
        public MenuBar()
        {
            _pendingMedia = new ConcurrentStack<IMedia>();
            Open = new RelayCommand((param) => this.OpenCommand());
            Close = new RelayCommand((param) => Application.Current.Shutdown());
            _clockTimer.Tick += (object sender, EventArgs e) =>
                {
                    ILibrary lib = ModuleManager.GetInstanceOf<ILibrary>("LibraryViewModel");
                    if (lib == null) return ;
                    IMedia media;
                    while (_pendingMedia.TryPop(out media))
                        media.AddToLibrary(lib);
                };
            _clockTimer.Start();
        }
        #endregion

        #region method
        private void OpenCommand()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            IMediaPlayer mediaPlayer = ModuleManager.GetInstanceOf<IMediaPlayer>("MusicPlayerViewModel");
            Thread action;

            ofd.Title = "Open file";
            if (ofd.ShowDialog() != true)
                return;
            string path = ofd.FileName;
            if (path == null)
                return;
            action = new Thread(() => 
            {
                IMedia media = null;
                IInfoMedia mediaInfo = ModuleManager.GetInstanceOf<IInfoMedia>("InfoMedia");

                if (mediaInfo.Open(path))
                {
                    if (KindOfImage(mediaInfo)) media = ModuleManager.GetInstanceOf<IMusicMedia>("ImageMedia");
                    if (KindOfMusic(mediaInfo)) media = ModuleManager.GetInstanceOf<IMusicMedia>("MusicMedia");
                    if (KindOfVideo(mediaInfo)) media = ModuleManager.GetInstanceOf<IVideoMedia>("VideoMedia");
                    if (media != null)
                    {

                        media.SetInfo(mediaInfo);
                        _pendingMedia.Push(media);
                    }
                    mediaInfo.Close();
                }
            });
            action.Start();
            if (mediaPlayer != null)
            {
                mediaPlayer.Source = path;
                mediaPlayer.Play.Execute(new object());
            }
        }
        #endregion
    }
}
