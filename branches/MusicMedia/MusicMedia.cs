using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaInfoLib;
using System.IO;
using MWMP.Models;
using MWMP.ViewModels;

namespace Medias
{
    public class MusicMedia : IMusicMedia
    {

        public MusicMedia()
        {
        }

        public bool Open(string path)
        {
            
            return true;
        }
        public string Codec { get; private set; }
        public string[] Performers { get; private set; }
        public string Title { get; private set; }
        public string Album { get; private set; }
        public string Genre { get; private set; }
        public int FileSize { get; private set; }
        public int Duration { get; private set; }
        public int Track { get; private set; }
        public string Extension { get; private set; }
        public string Filename { get; private set; }
        public string Path { get; private set; }

        public void AddToLibrary(ILibrary lib)
        {
            lib.Add(this);
        }

        public void SetInfo(IInfoMedia media)
        {
            int value;
            Codec = media.Get("Codec");
            Performers = media.Get("Performer").Split('/');
            Title = media.Get("Title");
            Album = media.Get("Album");
            Genre = media.Get("Genre");
            FileSize = (Int32.TryParse(media.Get("FileSize"), out value)) ? value : 0;
            Duration = (Int32.TryParse(media.Get("Duration"), out value)) ? value : 0;
            Track = (Int32.TryParse(media.Get("Track/Position"), out value)) ? value : 0;
            Extension = media.Get("FileExtension");
            Filename = media.Get("FileName");
        }
    }
}
