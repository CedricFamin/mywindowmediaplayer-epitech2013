using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;
using MWMP;
using MWMP.Utils;
using MWMP.ViewModels;
using System.Windows.Data;

namespace LibraryViewModel
{
    abstract class GenericLibrary<T> : BindableObject, ILibrary<T>
    {
        #region Method
        private Dictionary<string, ICollectionView> CollectionViewCache { get; set; }
        #endregion

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
            CollectionViewCache = new Dictionary<string, ICollectionView>();
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
            if (this.CanAdd(media))
            {
                MediaList.Add(media);
                RaisePropertyChange("MediaList");
            }
        }

        abstract protected bool CanAdd(T media);
        #endregion

        public ICollectionView GetFilterCollectionView(string pptName, string value)
        {
            PropertyInfo property;
            Type type = typeof(T);
            try    { property = type.GetProperty(pptName); }
            catch  { return null; }
            if (property == null) return null;
            ICollectionView cv = CollectionViewSource.GetDefaultView(MediaList);
            cv.Filter = new Predicate<object>(FilterOperator);
            return cv;
        }

        public abstract bool FilterOperator(object source);
    }
}
