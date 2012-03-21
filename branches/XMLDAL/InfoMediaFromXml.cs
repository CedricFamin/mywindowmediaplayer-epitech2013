using System;
using System.Reflection;
using System.Xml.Linq;
using MWMP.Models;

namespace XMLDAL
{
    class InfoMediaFromXml : IInfoMedia
    {
        public XElement Infos { get; set; }
 
        private string Get(string option)
        {
            XElement element = Infos.Element(option);
            if (element == null)
                return "";
            return element.Value;
        }

        #region Useless Method
        public bool Open(string path)
        {
            return true;
        }

        public void Close()
        {
            ;
        }
        #endregion

        #region generic loading method
        private void _load<T>(T media)
        {
            Type type = typeof(T);
            foreach (PropertyInfo property in type.GetProperties())
            {
                MethodInfo method = property.GetSetMethod();
                object[] parameters = new object[1];
                try
                {
                    parameters[0] =  Convert.ChangeType(Get(property.Name), property.PropertyType);
                }
                catch
                {
                    continue;
                }
                method.Invoke(media, parameters);
            }
        }
        #endregion

        #region SetInfo Method
        public void SetInfo(IVideoMedia media)
        {
            _load<IMedia>(media);
            _load<IVideoMedia>(media);
        }

        public void SetInfo(IMusicMedia media)
        {
            _load<IMedia>(media);
            _load<IMusicMedia>(media);
        }

        public void SetInfo(IImageMedia media)
        {
            _load<IMedia>(media);
            _load<IImageMedia>(media);
        }
        #endregion

        #region unused
        public string InternetMediaType
        {
            get { return Get("InternetMediaType"); }
        }
        #endregion
    }
}
