namespace MWMP.Models
{
    public interface IMediaFactory
    {
        IMusicMedia CreateMusic(IInfoMedia infoMedia = null);
        IVideoMedia CreateVideo(IInfoMedia infoMedia = null);
        IImageMedia CreateImage(IInfoMedia infoMedia = null);
        IMedia CreateWithInternetMediaType(string IMType, IInfoMedia infoMedia = null);
    }
}
