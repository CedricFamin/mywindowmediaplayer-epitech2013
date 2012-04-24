using System.ComponentModel;
using System.Windows.Input;
using MWMP.Models;

namespace MWMP.ViewModels
{
    public interface IMusicLibrary : ILibrary<IMusicMedia>
    {
        ICollectionView FilteredMediaList { get; }
        ICommand SetTitleFilter { get; }
        ICommand SetPerformerFilter { get; }
        ICommand SetGenreFilter { get; }

        string Performer { get; set; }
        string Genre { get; set; }
        string Title { get; set; }
    }
}
