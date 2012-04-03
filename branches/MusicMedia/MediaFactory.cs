using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Models;

namespace Medias
{
    class MediaFactory : IMediaFactory
    {
        #region Methods
        public IMusicMedia CreateMusic(IInfoMedia infoMedia = null)
        {
            IMusicMedia media = new MusicMedia();
            if (infoMedia != null)
                infoMedia.SetInfo(media);
            return media;
        }

        public IVideoMedia CreateVideo(IInfoMedia infoMedia = null)
        {
            IVideoMedia media = new VideoMedia();
            if (infoMedia != null)
                infoMedia.SetInfo(media);
            return media;
        }

        public IImageMedia CreateImage(IInfoMedia infoMedia = null)
        {
            throw new NotImplementedException();
        }

        public IMedia CreateWithInternetMediaType(string IMType, IInfoMedia infoMedia = null)
        {
            if (IMType.Contains("image")) return CreateImage(infoMedia);
            if (IMType.Contains("audio")) return CreateMusic(infoMedia);
            if (IMType.Contains("video")) return CreateVideo(infoMedia);
            return null;
        }
        #endregion
    }
}
