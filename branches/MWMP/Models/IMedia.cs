namespace MWMP.Models
{
    public interface IMedia
    {
        string Title { get; set; }
        string Extension { get; set; }
        string Filename { get; set; }
        string Path { get; set; }
        int FileSize { get; set; }
    }
}
