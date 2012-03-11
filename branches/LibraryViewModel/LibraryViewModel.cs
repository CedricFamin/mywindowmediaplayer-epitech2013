using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.ViewModels;
using MWMP.Models;
using MWMP;
using System.Collections;

namespace LibraryViewModel
{
    public class LibraryViewModel : BindableObject, ILibraryViewModel
    {

        public LibraryViewModel()
        {
            PlayList = new List<IMusicMedia>();
            IMusicMedia media = ModuleManager.getInstanceOf<IMusicMedia>("MusicMedia");
            media.Open("music.mp3");
            PlayList.Add(media);
            Position = 0;
        }

        private List<IMusicMedia> PlayList { get; set; }
        public int Position { get; private set; }

        public IMusicMedia CurrentMedia
        {
            get
            {
                if (Count > 0)
                    return PlayList[Position];
                return null;
            }
        }

        public int Count
        {
            get { return PlayList.Count; }
        }

        public void Add(IMusicMedia media)
        {
            if (Count == 0)
                Position = 0;
            PlayList.Add(media);
            RaisePropertyChange("Collection");
        }

        public void Del(int position)
        {
            if (position < Count && position > 0)
            {
                if (position == Position)
                    Next();
                PlayList.RemoveAt(position);
                Position--;
            }
        }

        public IMusicMedia Next()
        {
            if (Count == 0)
                return null;
            Position++;
            if (Position >= Count)
                Position = 0;
            return PlayList[Position];
        }

        public IMusicMedia Previous()
        {
            if (Count == 0)
                return null;
            Position--;
            if (Position < 0)
                Position = Count - 1;
            return PlayList[Position];
        }


        public ICollection<IMusicMedia> Collection
        {
            get { return (ICollection<IMusicMedia>)PlayList; }
        }
    }
}
