using System.ComponentModel;
using MWMP;
using MWMP.Models;
using MWMP.Models.DAL;
using MWMP.ViewModels;

namespace LibraryViewModel
{
    class GlobalLibrary : IGlobalLibrary
    {
        #region Properties
        public ILibrary<IMusicMedia> MusicLibrary { get; private set; }
        public ILibrary<IVideoMedia> VideoLibrary { get; private set; }
        public ILibrary<IImageMedia> ImageLibrary { get; private set; }
        public ILibrary<IPlayList> PlayListLibrary { get; private set; }
        #endregion

        #region CTor
        public GlobalLibrary()
        {
            MusicLibrary = ModuleManager.GetInstanceOf<ILibrary<IMusicMedia>>("MusicLibrary");
            VideoLibrary = ModuleManager.GetInstanceOf<ILibrary<IVideoMedia>>("VideoLibrary");
            ImageLibrary = ModuleManager.GetInstanceOf<ILibrary<IImageMedia>>("ImageLibrary");
            PlayListLibrary = ModuleManager.GetInstanceOf<ILibrary<IPlayList>>("PlayListLibrary");

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


        public ICollectionView MusicFilter
        {
            get { return MusicLibrary.GetFilterCollectionView("Title", ""); }
        }
    }
}
