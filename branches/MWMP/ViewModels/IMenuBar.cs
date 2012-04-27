using System.Windows.Input;

namespace MWMP.ViewModels
{
    public interface IMenuBar
    {
        ICommand Open { get; }
        ICommand Close { get; }
        ICommand OpenAboutWindow { get; }
    }
}
