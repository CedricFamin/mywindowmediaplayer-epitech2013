using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWMP.Models
{
    public interface IPlayer
    {
        void Play();
        void Pause();
        void Stop();
        void GoTo(int time);
        void Volume(int volume);
        void Load(string path);
        void Mute();
    }
}
