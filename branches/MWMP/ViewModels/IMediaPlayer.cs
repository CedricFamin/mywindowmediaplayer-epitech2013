using System.Windows.Input;
using MWMP.Models;

namespace MWMP.ViewModels
{
    public interface IMediaPlayer
    {
        string Source { set; get; }
        int CurrentMedia { get; set; }
        IPlayList PlayList { get;}

        void LoadedMediaFaild();

        ICommand ChangePist { get; }
        ICommand Next { get; }
        ICommand Previous { get; }
        ICommand Open { get; }
        ICommand AddMediaToPlayList { get; }
    }
}
