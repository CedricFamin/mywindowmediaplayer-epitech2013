using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Models;
using System.ComponentModel;
using System.Windows.Input;

namespace MWMP.ViewModels
{
    public interface IMusicLibrary : ILibrary<IMusicMedia>
    {
        ICollectionView FilteredMediaList { get; }
        ICommand SetTitleFilter { get; }

        string Performer { get; set; }
        string Genre { get; set; }
        string Title { get; set; }
    }
}
