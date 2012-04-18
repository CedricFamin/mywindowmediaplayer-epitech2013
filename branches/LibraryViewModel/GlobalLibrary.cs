using System.ComponentModel;
using MWMP;
using MWMP.Models;
using MWMP.Models.DAL;
using MWMP.ViewModels;
using System.Windows.Input;
using MWMP.Utils;
using System.Windows.Controls;
using System.Windows;
using System;

namespace LibraryViewModel
{
    class GlobalLibrary : BindableObject, IGlobalLibrary
    {
        #region Properties
        public IMusicLibrary MusicLibrary { get; private set; }
        public ILibrary<IVideoMedia> VideoLibrary { get; private set; }
        public ILibrary<IImageMedia> ImageLibrary { get; private set; }
        public ILibrary<IPlayList> PlayListLibrary { get; private set; }

        public ICommand DropData { get; private set; }
        public ICommand BeginDragData { get; private set; }
        public ICommand Display { get; private set; }
        public ICommand CreatePlaylist { get; private set; }
        public ICommand OpenPlayListWindow { get; private set; }
        public ICommand PlayPlayList { get; private set; }
        #endregion

        #region CTor
        public GlobalLibrary()
        {
            MusicLibrary = ModuleManager.GetInstanceOf<IMusicLibrary>("MusicLibrary");
            VideoLibrary = ModuleManager.GetInstanceOf<ILibrary<IVideoMedia>>("VideoLibrary");
            ImageLibrary = ModuleManager.GetInstanceOf<ILibrary<IImageMedia>>("ImageLibrary");
            PlayListLibrary = ModuleManager.GetInstanceOf<ILibrary<IPlayList>>("PlayListLibrary");
            CreatePlaylist = new RelayCommand((param) => CreatePlaylistBody(param as string));
            Display = new RelayCommand((param) => DisplayBody(param as string));
            OpenPlayListWindow = new RelayCommand((param) => OpenPlayListWindowBody());
            PlayPlayList = new RelayCommand((param) => PlayPlayListBody(param as IPlayList));
            BeginDragData = new RelayCommand((param) => BeginDragDataBody(param as FrameworkElement));
            DropData = new RelayCommand((param) => DropDataBody(param as object[]));
            IDAL dal = ModuleManager.GetInstanceOf<IDAL>("XMLDAL");
            if (dal != null)
            {
                foreach (IMusicMedia media in dal.MusicList)
                    MusicLibrary.MediaList.Add(media);
                foreach (IVideoMedia media in dal.VideoList)
                    VideoLibrary.MediaList.Add(media); 
                foreach (IImageMedia media in dal.ImageList)
                    ImageLibrary.MediaList.Add(media);
                foreach (IPlayList plist in dal.PlayListList)
                    PlayListLibrary.MediaList.Add(plist);
            }
        }
        #endregion

        #region DTor
        ~GlobalLibrary()
        {
            IDAL dal = ModuleManager.GetInstanceOf<IDAL>("XMLDAL");
            if (dal != null)
            {
                foreach (IMusicMedia media in MusicLibrary.MediaList)
                    dal.Save(media, "audio");
                foreach (IVideoMedia media in VideoLibrary.MediaList)
                    dal.Save(media, "video");
                foreach (IImageMedia media in ImageLibrary.MediaList)
                    dal.Save(media, "image");
                foreach (IPlayList plist in PlayListLibrary.MediaList)
                    dal.Save(plist);
                dal.Save();
            }
        }
        #endregion

        #region Method
        public void AddMedia(IMedia media)
        {
            IMusicMedia musicMedia = media as IMusicMedia;
            IVideoMedia videoMedia = media as IVideoMedia;
            IImageMedia imageMedia = media as IImageMedia;

            if (musicMedia != null) MusicLibrary.Add(musicMedia);
            else if (videoMedia != null) VideoLibrary.Add(videoMedia);
            else if (imageMedia != null) ImageLibrary.Add(imageMedia);
        }
        #endregion

        #region Private methods
        void DisplayBody(string lib)
        {
            if (lib == null)
                return;
            MusicLibrary.Visibility = (lib == "MusicLib") ? "Visible" : "Hidden";
            VideoLibrary.Visibility = (lib == "VideoLib") ? "Visible" : "Hidden";
            ImageLibrary.Visibility = (lib == "ImageLib") ? "Visible" : "Hidden";
            PlayListLibrary.Visibility = (lib == "PlayListLib") ? "Visible" : "Hidden";
        }
        private void CreatePlaylistBody(string name)
        {
            if (name == null)
                return;
            IPlayList plist = ModuleManager.GetInstanceOf<IPlayList>("PlayList");
            if (plist == null)
                return;
            plist.Title = name;
            PlayListLibrary.Add(plist);
        }
        private void PlayPlayListBody(IPlayList plist)
        {
            if (plist == null)
                return;
            IMediaPlayer mp = ModuleManager.GetInstanceOf<IMediaPlayer>("MusicPlayerViewModel");
            if (mp != null)
            {
                foreach (IMedia media in plist.Collection)
                    mp.AddMediaToPlayList.Execute(media);
            }
        }
        private void OpenPlayListWindowBody()
        {
            CreatePlayListWindows window = new CreatePlayListWindows();
            window.ShowDialog();
        }
        private void BeginDragDataBody(FrameworkElement element)
        {
            if (element == null)
                return;
            IMedia media = element.DataContext as IMedia;
            if (media == null) return;
            MediaWrapper mediaWrapper = new MediaWrapper()
            {
                Media = media
            };
            DragDrop.DoDragDrop(element, mediaWrapper, DragDropEffects.Move);
        }
        private void DropDataBody(object[] p)
        {
            if (p == null)
                return;
            DragEventArgs args = p[0] as DragEventArgs;
            IPlayList plist = p[2] as IPlayList;
            if (args == null)
                return;
            if (plist == null)
                return;
            MediaWrapper media = args.Data.GetData(typeof(MediaWrapper)) as MediaWrapper;
            plist.Add(media.Media);
        }
        #endregion

        #region IMediaWrapper
        class MediaWrapper
        {
            public IMedia Media { get; set; }
        }
        #endregion
    }
}
