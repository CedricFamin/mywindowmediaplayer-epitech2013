using MWMP.Models;

namespace Medias
{
    public class MusicMedia : IMusicMedia
    {

        #region CTor
        public MusicMedia()
        {
        }
        #endregion

        #region properties
        public string Codec { get; set; }
        public string Performers { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public int FileSize { get; set; }
        public int Duration { get; set; }
        public int Track { get; set; }
        public string Extension { get; set; }
        public string Filename { get; set; }
        public string Path { get; set; }
        #endregion

        #region Methods
        public void SetInfo(IInfoMedia infoMedia)
        {
            infoMedia.SetInfo(this);
        }
        #endregion
    }
}
