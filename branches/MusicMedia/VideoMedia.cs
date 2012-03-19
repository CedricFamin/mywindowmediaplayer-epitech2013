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
        public VideoMedia()
        {

        }

        public string Title { get; private set; }
        public string Genre { get; private set; }
        public int Duration { get; private set; }
        public string Extension { get; private set; }
        public string Filename { get; private set; }
        public string Path { get; private set; }
        public int FileSize { get; private set; }

        public void AddToLibrary(ILibrary lib)
        {
            lib.Add(this);
        }

        public void SetInfo(IInfoMedia media)
        {
            int value;

            Filename = media.Get("FileName");
            Title = media.Get("Movie");
            Title = (string.IsNullOrEmpty(Title)) ? Filename : Title;
            Genre = media.Get("Genre");
            FileSize = (Int32.TryParse(media.Get("FileSize"), out value)) ? value : 0;
            Duration = (Int32.TryParse(media.Get("Duration"), out value)) ? value : 0;
            Extension = media.Get("FileExtension");
        }
    }
}
