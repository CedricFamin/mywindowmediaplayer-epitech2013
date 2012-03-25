using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Models;

namespace MenuBar
{
    class BasicMedia : IMedia
    {
        public string Extension { get; set; }
        public string Filename { get; set; }
        public string Path { get; set; }
        public int FileSize { get; set; }
        public void SetInfo(IInfoMedia infoMedia) { throw new NotImplementedException(); }
    }
}
