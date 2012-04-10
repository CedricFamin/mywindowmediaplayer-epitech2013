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

namespace LibraryViewModel
{
    class MusicLibrary : GenericLibrary<IMusicMedia>
    {
        protected override bool CanAdd(IMusicMedia media)
        {
            foreach (IMusicMedia currentMedia in MediaList)
            {
                if (currentMedia.Path == media.Path)
                    return false;
            }
            return true;
        }

        public override bool FilterOperator(object source)
        {
            IMusicMedia media = source as IMusicMedia;
            if (media.Title.Contains("Sleep")) return true;
            return false;
        }
    }
}
