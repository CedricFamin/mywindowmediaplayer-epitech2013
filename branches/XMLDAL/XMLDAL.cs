using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using MWMP;
using MWMP.Models;
using MWMP.Models.DAL;

namespace XMLDAL
{
    public class XMLDAL : IDAL
    {
        #region properties
        private XElement _root {get;set;}
        public IList<IMedia> MediaList { get; private set; }
        #endregion

        #region Ctor
        public XMLDAL()
        {
            _root = new XElement("medias");
            MediaList = new List<IMedia>();
            if (File.Exists("data.xml") == false) return;
            InfoMediaFromXml mediaInfo = new InfoMediaFromXml();
            XElement root;
            try { root = XElement.Load("data.xml"); }
            catch { return; }
            IEnumerable<XElement> medias = from els in root.Elements("media") select els;

            foreach (XElement media in medias)
            {
                IMedia realMedia = ModuleManager.GetInstanceOf<IMedia>(media.Attribute("type").Value);
                if (realMedia == null) continue;
                mediaInfo.Infos = media;
                realMedia.SetInfo(mediaInfo);
                MediaList.Add(realMedia);
            }
        }
        #endregion

        #region Save method
        private void _save<T>(T media, XElement e)
        {
            Type type = typeof(T);
            foreach (PropertyInfo property in type.GetProperties())
            {
                MethodInfo method = property.GetGetMethod();
                object value = method.Invoke(media, new object[0]);
                XElement CurrentElement = new XElement(property.Name, value);
                e.Add(CurrentElement);
            }
        }

        public void Save<T>(T media, string serviceName) where T : IMedia
        {
            XElement medias = _root;
            XElement Element = new XElement("media");
            Type type = typeof(T);
            _save<IMedia>(media, Element);
            _save<T>(media, Element);
            Element.SetAttributeValue("type", serviceName);
            medias.Add(Element);
        }

        public void Save()
        {
            _root.Save("data.xml");
        }
        #endregion
    }
}
