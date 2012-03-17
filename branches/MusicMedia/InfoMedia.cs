using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Models;
using MediaInfoLib;

namespace Medias
{
    class InfoMedia : IInfoMedia
    {
        private MediaInfo _mediaInfo { get; set; }

        public InfoMedia()
        {
            _mediaInfo = new MediaInfo();
        }

        public string Get(string option)
        {
            return _mediaInfo.Get(0, 0, option);
        }


        public bool Open(string path)
        {
            return _mediaInfo.Open(path) >= 0;
        }

        public void Close()
        {
            _mediaInfo.Close();
        }
    }
}
