using System.Windows.Input;
using MWMP;
using MWMP.Models;
using MWMP.Utils;
using MWMP.ViewModels;

namespace LibraryViewModel
{
    class PlayListLibrary : GenericLibrary<IPlayList>, IPlayListLibrary
    {
        public ICommand ChangeSelectedItem { get; private set; }

        #region CTor
        public PlayListLibrary() : base()
        {
            ChangeSelectedItem = new RelayCommand((param) => ChangeSelectedItemBody(param as IPlayList));
        }
        #endregion

        #region Methods
        private void ChangeSelectedItemBody(IPlayList plist)
        {
            if (plist == null)
                return;
            SelectedItem = plist;
            RaisePropertyChange("SelectedItem");
        }

        protected override void AddToPlayListBody()
        {
            IMediaPlayer mp = ModuleManager.GetInstanceOf<IMediaPlayer>("MusicPlayerViewModel");
            if (mp != null && SelectedItem != null)
            {
                foreach (IMedia media in SelectedItem.Collection)
                    mp.AddMediaToPlayList.Execute(media);
            }
        }

        protected override void PlayContextMenuBody()
        {
            IMediaPlayer mp = ModuleManager.GetInstanceOf<IMediaPlayer>("MusicPlayerViewModel");
            bool first = true;
            if (mp != null && SelectedItem != null)
            {
                foreach (IMedia media in SelectedItem.Collection)
                {
                    if (first) mp.Open.Execute(media);
                    else mp.AddMediaToPlayList.Execute(media);
                    first = false;
                }
            }
        }

        protected override bool CanAdd(IPlayList media)
        {
            return true;
        }
        #endregion
    }
}
