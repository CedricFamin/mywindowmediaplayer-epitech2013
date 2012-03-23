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
        #region properties
        private MediaInfo _mediaInfo { get; set; }
        private string _path { get; set; }
        #endregion

        #region Ctor
        public InfoMedia()
        {
            _mediaInfo = new MediaInfo();
        }
        #endregion

        #region public method
        public bool Open(string path)
        {
            _path = path;
            return _mediaInfo.Open(path) >= 0;
        }

        public void Close()
        {
            _mediaInfo.Close();
        }

        public string InternetMediaType { get { return _mediaInfo.Get(0, 0, "InternetMediaType"); } }
        #endregion

        #region SetInfo method
        public void SetInfo(IVideoMedia media)
        {
            int value;

            media.Filename = _mediaInfo.Get(0, 0, "FileName");
            media.Title = _mediaInfo.Get(0, 0, "Movie");
            media.Title = (string.IsNullOrEmpty(media.Title)) ? media.Filename : media.Title;
            media.Genre = _mediaInfo.Get(0, 0, "Genre");
            media.FileSize = (Int32.TryParse(_mediaInfo.Get(0, 0, "FileSize"), out value)) ? value : 0;
            media.Duration = (Int32.TryParse(_mediaInfo.Get(0, 0, "Duration"), out value)) ? value : 0;
            media.Extension = _mediaInfo.Get(0, 0, "FileExtension");
            media.Path = _path;
        }

        public void SetInfo(IMusicMedia media)
        {
            int value;

            media.Codec = _mediaInfo.Get(0, 0, "Codec");
            media.Performers = _mediaInfo.Get(0, 0, "Performer");
            media.Title = _mediaInfo.Get(0, 0, "Title");
            media.Album = _mediaInfo.Get(0, 0, "Album");
            media.Genre = _mediaInfo.Get(0, 0, "Genre");
            media.FileSize = (Int32.TryParse(_mediaInfo.Get(0, 0, "FileSize"), out value)) ? value : 0;
            media.Duration = (Int32.TryParse(_mediaInfo.Get(0, 0, "Duration"), out value)) ? value : 0;
            media.Track = (Int32.TryParse(_mediaInfo.Get(0, 0, "Track/Position"), out value)) ? value : 0;
            media.Extension = _mediaInfo.Get(0, 0, "FileExtension");
            media.Filename = _mediaInfo.Get(0,0, "FileName");
            media.Path = _path;
        }

        public void SetInfo(IImageMedia media)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
