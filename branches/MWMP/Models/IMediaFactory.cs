using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWMP.Models
{
    public interface IMediaFactory
    {
        IMusicMedia CreateMusic();
        IVideoMedia CreateVideo();
        IImageMedia CreateImage();
        IMedia CreateWithInternetMediaType(string IMType);
    }
}
