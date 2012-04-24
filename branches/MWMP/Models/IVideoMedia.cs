namespace MWMP.Models
{
    public interface IVideoMedia : IMedia
    {
        string Genre { get; set; }
        int Duration { get; set; }
        int Width { get; set; }
        int Height { get; set; }
    }
}
