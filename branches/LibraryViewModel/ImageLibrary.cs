using MWMP.Models;

namespace LibraryViewModel
{
    class ImageLibrary : GenericLibrary<IImageMedia>
    {
        protected override bool CanAdd(IImageMedia media)
        {
            foreach (IImageMedia currentMedia in MediaList)
            {
                if (currentMedia.Path == media.Path)
                    return false;
            }
            return true;
        }
    }
}
