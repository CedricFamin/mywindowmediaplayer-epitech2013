﻿using System;
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
    class PlayListLibrary : GenericLibrary<IPlayList>
    {
        public new IPlayList SelectedItem { get; set; }
        public new ICommand PlayContextMenu { get; private set; }
        public new ICommand AddToPlayList { get; private set; }

        public PlayListLibrary() : base()
        {
            PlayContextMenu = new RelayCommand((param) =>
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
            });
            AddToPlayList = new RelayCommand((param) =>
            {
                IMediaPlayer mp = ModuleManager.GetInstanceOf<IMediaPlayer>("MusicPlayerViewModel");
                if (mp != null && SelectedItem != null)
                {
                    foreach (IMedia media in SelectedItem.Collection)
                        mp.AddMediaToPlayList.Execute(media);
                }
            });
        }

        protected override bool CanAdd(IPlayList media)
        {
            return true;
        }
    }
}
