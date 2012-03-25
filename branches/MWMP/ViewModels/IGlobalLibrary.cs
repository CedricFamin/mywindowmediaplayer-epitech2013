using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Models;

namespace MWMP.ViewModels
{
    public interface IGlobalLibrary
    {
        ILibrary<IMusicMedia> MusicLibrary { get; }
        ILibrary<IVideoMedia> VideoLibrary { get; }
        ILibrary<IImageMedia> ImageLibrary { get; }

        void AddMedia(IMedia media);
    }
}
