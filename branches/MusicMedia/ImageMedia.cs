using MWMP.Models;

namespace Medias
{
    public class ImageMedia : IImageMedia
    {
        public string Title { get; set; }
        public string Extension { get; set; }
        public string Filename { get; set; }
        public string Path { get; set; }
        public int FileSize { get; set; }
    }
}
