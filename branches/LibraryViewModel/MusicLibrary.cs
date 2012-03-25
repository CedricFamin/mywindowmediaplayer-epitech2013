using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.ViewModels;
using MWMP.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MWMP.Utils;
using MWMP;
using MWMP.Models.DAL;

namespace LibraryViewModel
{
    class MusicLibrary : BindableObject, ILibrary<IMusicMedia>
    {
        #region Properties
        public ObservableCollection<IMusicMedia> MediaList { get; private set; }
        public IMedia SelectedItem { get; set; }
        public ICommand PlayContextMenu { get; private set; }
        #endregion

        #region Ctor
        public MusicLibrary()
        {
            MediaList = new ObservableCollection<IMusicMedia>();
            PlayContextMenu = new RelayCommand((param) =>
                {
                    IMediaPlayer mp = ModuleManager.GetInstanceOf<IMediaPlayer>("MusicPlayerViewModel");
                    if (mp != null && SelectedItem != null)
                        mp.Open.Execute(SelectedItem);
                });
            IDAL dal = ModuleManager.GetInstanceOf<IDAL>("XMLDAL");
            foreach (IMusicMedia media in dal.MusicList)
            {
                MediaList.Add(media);
            }
        }
        #endregion

        #region Method
        public void Add(IMusicMedia media)
        {
            MediaList.Add(media);
        }
        #endregion
    }
}
