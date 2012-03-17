using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Models;
using System.Collections.ObjectModel;

namespace MWMP.ViewModels
{
    public interface ILibrary
    {
        ObservableCollection<IMusicMedia> MusicList { get; }
        ObservableCollection<IVideoMedia> VideoList { get; }
        ObservableCollection<IImageMedia> ImageList { get; }

        void Add(IMusicMedia media);
        void Add(IVideoMedia media);
        void Add(IImageMedia media);
    }
}
