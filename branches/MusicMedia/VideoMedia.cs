using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaInfoLib;
using MWMP.Models;
using MWMP.ViewModels;
using MWMP.Models.DAL;

namespace Medias
{
    class VideoMedia : IVideoMedia
    {
        #region CTor
        public VideoMedia()
        {
        }
        #endregion

        #region properties
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Duration { get; set; }
        public string Extension { get; set; }
        public string Filename { get; set; }
        public string Path { get; set; }
        public int FileSize { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        #endregion

        #region Methods
        public void SetInfo(IInfoMedia infoMedia)
        {
            infoMedia.SetInfo(this);
        }
        #endregion
    }
}
