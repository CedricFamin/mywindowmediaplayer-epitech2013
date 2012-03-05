using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWMP.Models
{
    interface IMedia
    {
        void loadOnPlayer(Player p);
        string Path { get; }
    }
}
