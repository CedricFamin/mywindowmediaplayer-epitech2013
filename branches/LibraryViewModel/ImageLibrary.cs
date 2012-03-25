using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Models;
using MWMP.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MWMP.Utils;
using MWMP;

namespace LibraryViewModel
{
    class ImageLibrary : BindableObject, ILibrary<IImageMedia>
    {
        #region Properties
        public ObservableCollection<IImageMedia> MediaList { get; private set; }
        public IMedia SelectedItem { get; set; }
        public ICommand PlayContextMenu { get; private set; }
        #endregion

        #region Ctor
        public ImageLibrary()
        {
            MediaList = new ObservableCollection<IImageMedia>();
            PlayContextMenu = new RelayCommand((param) =>
                {
                    IMediaPlayer mp = ModuleManager.GetInstanceOf<IMediaPlayer>("MusicPlayerViewModel");
                    if (mp != null && SelectedItem != null)
                        mp.Open.Execute(SelectedItem);
                });
        }
        #endregion

        #region Method
        public void Add(IImageMedia media)
        {
            MediaList.Add(media);
            RaisePropertyChange("MediaList");
        }
        #endregion
    }
}
