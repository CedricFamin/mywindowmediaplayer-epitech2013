using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Models;
using System.ComponentModel;
using System.Windows.Input;

namespace MWMP.ViewModels
{
    public interface IGlobalLibrary
    {
        IMusicLibrary MusicLibrary { get; }
        ILibrary<IVideoMedia> VideoLibrary { get; }
        ILibrary<IImageMedia> ImageLibrary { get; }
        ILibrary<IPlayList> PlayListLibrary { get; }

        ICommand DropData { get; }
        ICommand BeginDragData { get; }
        ICommand CreatePlaylist { get; }
        ICommand PlayPlayList { get; }
        ICommand OpenPlayListWindow { get; }
        ICommand Display { get; }

        void AddMedia(IMedia media);
    }
}
