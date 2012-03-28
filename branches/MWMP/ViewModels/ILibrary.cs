using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MWMP.ViewModels
{
    public interface ILibrary<T> where T : IMedia
    {
        ObservableCollection<T> MediaList { get; }
        T SelectedItem { get; set; }
        ICommand PlayContextMenu { get; }
        ICommand AddToPlayList { get; }
        ICommand DeleteContextMenu { get; }
        void Add(T media);
    }
}
