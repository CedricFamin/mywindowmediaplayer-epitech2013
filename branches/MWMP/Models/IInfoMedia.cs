using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWMP.Models
{
    public interface IInfoMedia
    {
        string Get(string option);
        bool Open(string path);
        void Close();
    }
}
