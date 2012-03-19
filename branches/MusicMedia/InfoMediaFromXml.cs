using System.Xml.Linq;
using MWMP.Models;

namespace Medias
{
    class InfoMediaFromXml : IInfoMedia
    {
        public XElement Infos { get; set; }
 
        public string Get(string option)
        {
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
