using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Models;
using System.Xml.Linq;
using System.Linq;

namespace XMLDAL
{
    class InfoMediaFromXml : IInfoMedia
    {
        public XElement Infos { get; set; }
 
        public string Get(string option)
        {
            int pos = option.IndexOf('/');
            if (pos >= 0)
                option = option.Remove(pos);
            XElement element = Infos.Element(option);
            if (element == null)
                return "";
            return element.Value;
        }

        public bool Open(string path)
        {
            return true;
        }

        public void Close()
        {
            ;
        }
    }
}
