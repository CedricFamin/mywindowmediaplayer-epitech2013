using MWMP.Models;

namespace LibraryViewModel
{
    class VideoLibrary : GenericLibrary<IVideoMedia>
    {
        protected override bool CanAdd(IVideoMedia media)
        {
            foreach (IVideoMedia currentMedia in MediaList)
            {
                if (currentMedia.Path == media.Path)
                    return false;
            }
            return true;
        }
    }
}
