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
        void SetInfo(IInfoMedia media);
        string Extension { get; }
        string Filename { get; }
        string Path { get; }
        int FileSize { get; }
        void AddToLibrary(ILibrary lib);
    }
}
