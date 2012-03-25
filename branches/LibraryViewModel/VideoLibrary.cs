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
using MWMP.Models.DAL;

namespace LibraryViewModel
{
    class VideoLibrary : BindableObject, ILibrary<IVideoMedia>
    {
        #region Properties
        public ObservableCollection<IVideoMedia> MediaList { get; private set; }
        public IMedia SelectedItem { get; set; }
        public ICommand PlayContextMenu { get; private set; }
        #endregion

        #region Ctor
        public VideoLibrary()
        {
            MediaList = new ObservableCollection<IVideoMedia>();
            PlayContextMenu = new RelayCommand((param) =>
                {
                    IMediaPlayer mp = ModuleManager.GetInstanceOf<IMediaPlayer>("MusicPlayerViewModel");
                    if (mp != null && SelectedItem != null)
                        mp.Open.Execute(SelectedItem);
                });
            IDAL dal = ModuleManager.GetInstanceOf<IDAL>("XMLDAL");
            foreach (IVideoMedia media in dal.VideoList)
            {
                MediaList.Add(media);
            }
        }
        #endregion

        #region Method
        public void Add(IVideoMedia media)
        {
            MediaList.Add(media);
        }
        #endregion
    }
}
