using System.Collections.Generic;

namespace MWMP.Models.DAL
{
    public interface IDAL
    {
        IList<IMusicMedia> MusicList { get; }
        IList<IVideoMedia> VideoList { get; }
        IList<IImageMedia> ImageList { get; }
        IList<IPlayList> PlayListList { get; }

        void Save<T>(T media, string serviceName) where T : IMedia;
        void Save(IPlayList plist);
        void Save();
    }
}
