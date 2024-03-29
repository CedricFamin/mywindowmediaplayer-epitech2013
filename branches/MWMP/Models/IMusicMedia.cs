﻿namespace MWMP.Models
{
    public interface IMusicMedia : IMedia
    {
        string Performers { get; set; }
        string Album { get; set; }
        string Genre { get; set; }
        string Codec { get; set; }
        int Duration { get; set; }
        int Track { get; set; }
    }
}
