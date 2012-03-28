using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.ViewModels;
using MWMP.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MWMP;
using MWMP.Utils;

namespace LibraryViewModel
{
    class GenericLibrary<T> : BindableObject, ILibrary<T> where T : IMedia
    {
        #region Properties
        public ObservableCollection<T> MediaList { get; private set; }
        public T SelectedItem { get; set; }
        public ICommand PlayContextMenu { get; private set; }
        public ICommand AddToPlayList { get; private set; }
        public ICommand DeleteContextMenu { get; private set; }
        #endregion

        #region Ctor
        public GenericLibrary()
        {
            MediaList = new ObservableCollection<T>();
            PlayContextMenu = new RelayCommand((param) =>
                {
                    IMediaPlayer mp = ModuleManager.GetInstanceOf<IMediaPlayer>("MusicPlayerViewModel");
                    if (mp != null && SelectedItem != null)
                        mp.Open.Execute(SelectedItem);
                });
            AddToPlayList = new RelayCommand((param) =>
                {
                    IMediaPlayer mp = ModuleManager.GetInstanceOf<IMediaPlayer>("MusicPlayerViewModel");
                    if (mp != null && SelectedItem != null)
                        mp.AddMediaToPlayList.Execute(SelectedItem);
                });
            DeleteContextMenu = new RelayCommand((param) =>
                {
                    IMediaPlayer mp = ModuleManager.GetInstanceOf<IMediaPlayer>("MusicPlayerViewModel");
                    if (mp != null && SelectedItem != null)
                        MediaList.Remove(SelectedItem);
                });
        }
        #endregion

        #region Method
        public void Add(T media)
        {
            MediaList.Add(media);
            RaisePropertyChange("MediaList");
        }
        #endregion
    }
}
