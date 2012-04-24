using System.Windows.Input;
using MWMP.Models;

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
