using System.Windows.Input;
using MWMP.Models;

namespace MWMP.ViewModels
{
    public interface IPlayListLibrary : ILibrary<IPlayList>
    {
        ICommand ChangeSelectedItem { get; }
    }
}
