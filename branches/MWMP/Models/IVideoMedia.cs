using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWMP.Models
{
    public interface IVideoMedia : IMedia
    {
        string Title { get; }
        string Genre { get; }
        int Duration { get; }
    }
}
