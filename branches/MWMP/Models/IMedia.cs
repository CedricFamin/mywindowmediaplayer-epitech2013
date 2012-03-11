using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWMP.Models
{
    public interface IMedia
    {
        bool Open(string path);
        string Extension { get; }
        string Filename { get; }
        string Path { get; }
        int FileSize { get; }
    }
}
