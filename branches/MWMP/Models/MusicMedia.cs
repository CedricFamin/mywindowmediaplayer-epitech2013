using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWMP.Models
{
    class MusicMedia : IMedia
    {
        public MusicMedia(string path)
        {
            Path = path;
        }

        public void loadOnPlayer(Player p)
        {
            p.SetPlayer(this);
        }

        public string Path { get; private set; }
    }
}
