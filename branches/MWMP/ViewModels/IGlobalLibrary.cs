using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Models;
using System.ComponentModel;

namespace MWMP.ViewModels
{
    public interface IGlobalLibrary
    {
        IMusicLibrary MusicLibrary { get; }
        ILibrary<IVideoMedia> VideoLibrary { get; }
        ILibrary<IImageMedia> ImageLibrary { get; }
        ILibrary<IPlayList> PlayListLibrary { get; }

        void AddMedia(IMedia media);
    }
}
