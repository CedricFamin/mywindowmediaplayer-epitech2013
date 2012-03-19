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
        private Dictionary<string, string> convertOption = new Dictionary<string, string>()
        {
            {"Track", "Track/Position"},
            {"Extension", "FileExtension"},
            {"Filename", "FileName"}
        };

        public InfoMedia()
        {
            _mediaInfo = new MediaInfo();
            _mediaInfo.Option("ParseSpeed", "0.1");
        }

        public string Get(string option)
        {
            string opt;

            if (this.convertOption.TryGetValue(option, out opt))
                option = opt;
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
