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
        public ICommand SetTitleFilter { get; private set; }

        public MusicLibrary()
        {
            SetTitleFilter = new RelayCommand((param) => Title = param as string);
            FilteredMediaList = CollectionViewSource.GetDefaultView(MediaList);
            Title = "";
            Performer = "";
            Genre = "";
            FilteredMediaList.Filter = new Predicate<object>((item) =>
                {
                    IMusicMedia media = item as IMusicMedia;
                    if (media == null)
                        return false;
                    if (media.Title.Contains(Title) == false)
                        return false;
                    if (media.Genre.Contains(Genre) == false)
                        return false;
                    if (media.Performers.Contains(Performer) == false)
                        return false;
                    return true;
                });
        }

        protected override bool CanAdd(IMusicMedia media)
        {
            foreach (IMusicMedia currentMedia in MediaList)
            {
                if (currentMedia.Path == media.Path)
                    return false;
            }
            return true;
        }

        public ICollectionView FilteredMediaList { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            { 
                _title = value;
                FilteredMediaList.Refresh();
            }
        }

        private string _performer;
        public string Performer
        {
            get { return _performer; }
            set { _performer = value; }
        }

        private string _genre;
        public string Genre
        {
            get { return _genre; }
            set { _genre = value; }
        }
    }
}
