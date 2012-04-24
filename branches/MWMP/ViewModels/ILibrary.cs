using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MWMP.ViewModels
{
    public interface ILibrary<T>
    {
        ObservableCollection<T> MediaList { get; }
        T SelectedItem { get; set; }

        ICommand PlayContextMenu { get; }
        ICommand AddToPlayList { get; }
        ICommand DeleteContextMenu { get; }

        string Visibility { get; set; }

        void Add(T media);
    }
}
