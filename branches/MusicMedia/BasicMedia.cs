using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Models;

namespace Medias
{
    class BasicMedia : IMedia
    {
        #region Properties
        public string Title { get; set; }
        public string Extension { get; set; }
        public string Filename { get; set; }
        public string Path { get; set; }
        public int FileSize { get; set; }
        public void SetInfo(IInfoMedia infoMedia) { throw new NotImplementedException(); }
        #endregion
    }
}
