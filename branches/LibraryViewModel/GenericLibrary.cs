using System.Collections.ObjectModel;
using System.Windows.Input;
using MWMP;
using MWMP.Utils;
using MWMP.ViewModels;

namespace LibraryViewModel
{
    abstract class GenericLibrary<T> : BindableObject, ILibrary<T> where T : class
    {
        #region Fields
        protected string _visibility;
        #endregion

        #region Properties
        public ObservableCollection<T> MediaList { get; private set; }
        public virtual T SelectedItem { get; set; }
        public virtual ICommand PlayContextMenu { get; protected set; }
        public virtual ICommand AddToPlayList { get; protected set; }
        public virtual ICommand DeleteContextMenu { get; protected set; }
        public virtual string Visibility 
        { 
            get
            {
                return _visibility;
            }
            set
            {
                _visibility = value;
                RaisePropertyChange("Visibility");
            }
        }
        #endregion

        #region Ctor
        public GenericLibrary()
        {
            _visibility = "Hidden";
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
                if (SelectedItem != null)
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
    }
}
