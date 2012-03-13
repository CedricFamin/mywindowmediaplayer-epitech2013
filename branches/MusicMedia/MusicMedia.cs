using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaInfoLib;
using System.IO;
using MWMP.Models;

namespace MusicMedia
{
    public class MusicMedia : IMusicMedia
    {
        MediaInfo _mediaInfo;

        public MusicMedia()
        {
            this._mediaInfo = new MediaInfo();
        }

        public bool Open(string path)
        {
            Path = path;
            return this._mediaInfo.Open(path) >= 0;
        }
        string Codec { get { return this._mediaInfo.Get(0, 0, "Codec"); } }
        public string[] Performers { get { return this._mediaInfo.Get(0, 0, "Performer").Split('/'); }  }
        public string Title        { get { return this._mediaInfo.Get(0, 0, "Title"); }  }
        public string Album        { get { return this._mediaInfo.Get(0, 0, "Album"); }  }
        public string Genre        { get { return this._mediaInfo.Get(0, 0, "Genre"); }  }
        public int FileSize        
        { 
            get 
            {
                try { return Convert.ToInt32(this._mediaInfo.Get(0, 0, "FileSize")); }
                catch (Exception e) { return 0; }
            }
        }
        public int Duration
        { 
            get 
            {
                try { return Convert.ToInt32(this._mediaInfo.Get(0, 0, "Duration")); }
                catch (Exception e) { return 0; }
            }
        }
        public int Track
        { 
            get 
            {
                try { return Convert.ToInt32(this._mediaInfo.Get(0, 0, "Track/Position")); }
                catch (Exception e) { return 0; }
            } 
        }
        public string Extension    { get { return this._mediaInfo.Get(0, 0, "FileExtension"); } }
        public string Filename     { get { return this._mediaInfo.Get(0, 0, "FileName"); } }
        public string Path         { get; private set; }
    }
}
