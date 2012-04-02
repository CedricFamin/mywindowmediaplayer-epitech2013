using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace MWMP.Models
{
    public interface IPlayList
    {
        string Title { get; set; }
        ObservableCollection<IMedia> Collection { get; }

        void Add(IMedia media);
        void Delete(IMedia media);
        void Clear();
    }
}
