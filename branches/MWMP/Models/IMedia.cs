using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.ViewModels;
using MWMP.Models.DAL;

namespace MWMP.Models
{
    public interface IMedia
    {
        string Title { get; set; }
        string Extension { get; set; }
        string Filename { get; set; }
        string Path { get; set; }
        int FileSize { get; set; }
        void SetInfo(IInfoMedia infoMedia);
    }
}
