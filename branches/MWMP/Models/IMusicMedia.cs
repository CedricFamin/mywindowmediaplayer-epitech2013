using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWMP.Models
{
    public interface IMusicMedia : IMedia
    {
        string[] Performers { get; }
        string Title { get; }
        string Album { get; }
        string Genre { get; }
        int Duration { get; }
        int Track { get; }
    }
}
