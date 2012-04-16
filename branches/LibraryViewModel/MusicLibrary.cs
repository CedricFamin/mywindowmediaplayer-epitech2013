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
using System.ComponentModel;
using System.Windows.Data;

namespace LibraryViewModel
{
    class MusicLibrary : GenericLibrary<IMusicMedia>, IMusicLibrary
    {
        #region Fields
        private string _title;
        private string _performer;
        private string _genre;
        #endregion

        #region Properties
        public ICommand SetTitleFilter { get; private set; }
        public ICommand SetGenreFilter { get; private set; }
        public ICommand SetPerformerFilter { get; private set; }
        public ICollectionView FilteredMediaList { get; private set; }
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                FilteredMediaList.Refresh();
            }
        }
        public string Performer
        {
            get { return _performer; }
            set 
            { 
                _performer = value;
                FilteredMediaList.Refresh();
            }
        }
        public string Genre
        {
            get { return _genre; }
            set 
            { 
                _genre = value;
                FilteredMediaList.Refresh();
            }
        }
        #endregion

        #region CTor
        public MusicLibrary()
        {
            SetTitleFilter = new RelayCommand((param) => Title = param as string);
            SetGenreFilter = new RelayCommand((param) => Genre = param as string);
            SetPerformerFilter = new RelayCommand((param) => Performer = param as string);
            FilteredMediaList = CollectionViewSource.GetDefaultView(MediaList);
            FilteredMediaList.Filter = new Predicate<object>((item) =>
                {
                    IMusicMedia media = item as IMusicMedia;
                    if (media == null)
                        return false;
                    if (Title != null && media.Title.Contains(Title) == false)
                        return false;
                    if (Genre != null && media.Genre.Contains(Genre) == false)
                        return false;
                    if (Performer != null && media.Performers.Contains(Performer) == false)
                        return false;
                    return true;
                });
        }
        #endregion

        #region Methods
        protected override bool CanAdd(IMusicMedia media)
        {
            foreach (IMusicMedia currentMedia in MediaList)
            {
                if (currentMedia.Path == media.Path)
                    return false;
            }
            return true;
        }
        #endregion
    }
}
