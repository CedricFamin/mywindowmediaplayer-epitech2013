using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace MWMP.Models.DAL
{
    public interface IDAL
    {
        IList<IMusicMedia> MusicList { get; }
        IList<IVideoMedia> VideoList { get; }
        IList<IImageMedia> ImageList { get; }

        void Save<T>(T media, string serviceName) where T : IMedia;
        void Save();
    }
}
