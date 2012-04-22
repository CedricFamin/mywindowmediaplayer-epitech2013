using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Models;
using MWMP;
using MWMP.Utils;
using MWMP.ViewModels;
using System.Windows.Input;

namespace LibraryViewModel
{
    class PlayListLibrary : GenericLibrary<IPlayList>, IPlayListLibrary
    {
        public override IPlayList SelectedItem { get; set; }
        public override ICommand PlayContextMenu { get; protected set; }
        public override ICommand AddToPlayList { get; protected set; }
        public ICommand ChangeSelectedItem { get; private set; }

        public PlayListLibrary() : base()
        {
            PlayContextMenu = new RelayCommand((param) => PlayContextMenuBody());
            AddToPlayList = new RelayCommand((param) => AddToPlayListBody());
            ChangeSelectedItem = new RelayCommand((param) => ChangeSelectedItemBody(param as IPlayList));
        }

        private void ChangeSelectedItemBody(IPlayList plist)
        {
            if (plist == null)
                return;
            SelectedItem = plist;
            RaisePropertyChange("SelectedItem");
        }

        private void AddToPlayListBody()
        {
            IMediaPlayer mp = ModuleManager.GetInstanceOf<IMediaPlayer>("MusicPlayerViewModel");
            if (mp != null && SelectedItem != null)
            {
                foreach (IMedia media in SelectedItem.Collection)
                    mp.AddMediaToPlayList.Execute(media);
            }
        }

        private void PlayContextMenuBody()
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
    }
}
