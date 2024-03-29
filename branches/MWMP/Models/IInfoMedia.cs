﻿namespace MWMP.Models
{
    public interface IInfoMedia
    {
        void SetInfo(IVideoMedia media);
        void SetInfo(IMusicMedia media);
        void SetInfo(IImageMedia media);
        void SetInfo(IMedia media);
        bool Open(string path);
        void Close();
        string InternetMediaType { get; }
    }
}
