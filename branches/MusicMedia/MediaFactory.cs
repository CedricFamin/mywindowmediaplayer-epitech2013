using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Models;

namespace Medias
{
    class MediaFactory : IMediaFactory
    {
        public IMusicMedia CreateMusic()
        {
            return new MusicMedia();
        }

        public IVideoMedia CreateVideo()
        {
            return new VideoMedia();
        }

        public IImageMedia CreateImage()
        {
            throw new NotImplementedException();
        }

        public IMedia CreateWithInternetMediaType(string IMType)
        {
            if (IMType.Contains("image")) return CreateImage();
            if (IMType.Contains("music")) return CreateMusic();
            if (IMType.Contains("video")) return CreateVideo();
            return null;
        }
    }
}
