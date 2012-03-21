﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaInfoLib;
using System.IO;
using MWMP.Models;
using MWMP.ViewModels;
using MWMP.Models.DAL;

namespace Medias
{
    public class MusicMedia : IMusicMedia
    {

        public MusicMedia()
        {
        }

        #region properties
        public string Codec { get; set; }
        public string[] Performers { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public int FileSize { get; set; }
        public int Duration { get; set; }
        public int Track { get; set; }
        public string Extension { get; set; }
        public string Filename { get; set; }
        public string Path { get; set; }
        #endregion

        public void AddToLibrary(ILibrary lib)
        {
            lib.Add(this);
        }

        public void SetInfo(IInfoMedia infoMedia)
        {
            infoMedia.SetInfo(this);
        }
    }
}
