using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Models;
using System.Collections.ObjectModel;

namespace Medias
{
    class PlayList : IPlayList
    {
        #region Properties
        public string Title { get; set; }
        public ObservableCollection<IMedia> Collection { get; private set; }
        #endregion

        public PlayList()
        {
            Collection = new ObservableCollection<IMedia>();
            Title = "PlayList";
        }

        #region Methods
        public void Add(IMedia media)
        {
            Collection.Add(media);
        }

        public void Delete(IMedia media)
        {
            Collection.Remove(media);
        }

        public void Clear()
        {
            Collection.Clear();
        }
        #endregion
    }
}
