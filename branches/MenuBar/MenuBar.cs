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
using MWMP.InjectionDepedency;

namespace MenuBar
{
    public class Module : GenericModule<MenuBar>
    {
        public Module() : base("MenuBar", true) { }
    }

    public class MenuBar : IMenuBar
    {

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
                    IMedia media;
                    while (_pendingMedia.TryPop(out media))
                    {

                        ModuleManager.GetInstanceOf<IGlobalLibrary>("GlobalLibrary").AddMedia(media);
                    }
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
                    media = ModuleManager.GetInstanceOf<IMediaFactory>("MediaFactory").CreateWithInternetMediaType(mediaInfo.InternetMediaType, mediaInfo);
                    if (media != null)
                        _pendingMedia.Push(media);
                    mediaInfo.Close();
                }
            });
            action.Start();
            if (mediaPlayer != null)
            {
                IMedia media = ModuleManager.GetInstanceOf<IMedia>("BasicMedia");
                media.Title = path;
                media.Path = path;
                media.Filename = System.IO.Path.GetFileName(path);
                media.Extension = System.IO.Path.GetExtension(path);
                media.FileSize = 0;
                mediaPlayer.Open.Execute(media);
            }
        }
        #endregion
    }
}
