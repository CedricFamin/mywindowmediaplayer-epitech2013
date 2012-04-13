using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using MWMP;
using MWMP.InjectionDepedency;
using MWMP.Models;
using MWMP.Models.DAL;
using System.Xml.Serialization;
using System.Xml;

namespace XMLDAL
{
    public class Module : GenericModule<XMLDAL>
    {
        public Module() : base("XMLDAL", true) { }
    }

    static class XmlSerializerExtension
    {
        public static XElement SerializeAsXElement(this XmlSerializer xs, object o)
        {
            XDocument d = new XDocument();
            using (XmlWriter w = d.CreateWriter()) xs.Serialize(w, o);
            XElement e = d.Root;
            e.Remove();
            return e;
        }
    }

    public class XMLDAL : IDAL
    {
        #region properties
        private XElement _root {get;set;}
        public IList<IMusicMedia> MusicList { get; private set; }
        public IList<IVideoMedia> VideoList { get; private set; }
        public IList<IImageMedia> ImageList { get; private set; }
        public IList<IPlayList> PlayListList { get; private set; }
        #endregion

        #region Ctor
        public XMLDAL()
        {
            XElement root;
            Action<XElement> action;
            Dictionary<string, Action<XElement>> AddList = new Dictionary<string, Action<XElement>>();
            AddList.Add("audio", (param) => this.AddMusicMedia(param));
            AddList.Add("video", (param) => this.AddVideoMedia(param));
            AddList.Add("image", (param) => this.AddImageMedia(param));
            AddList.Add("playList", (param) => this.AddPlayList(param));

            _root = new XElement("medias");
            MusicList = new List<IMusicMedia>();
            VideoList = new List<IVideoMedia>();
            ImageList = new List<IImageMedia>();
            PlayListList = new List<IPlayList>();
            
            try { root = XElement.Load("data.xml"); }
            catch { return; }
            IEnumerable<XElement> medias = from els in root.Elements("media") select els;

            foreach (XElement media in medias)
            {
               if (AddList.TryGetValue(media.Attribute("type").Value, out action))
                   action(media);
            }
        }
        #endregion
        
        #region private methods
        private void AddMusicMedia(XElement media)
        {
            InfoMediaFromXml mediaInfo = new InfoMediaFromXml();
            IMediaFactory mfactory = ModuleManager.GetInstanceOf<IMediaFactory>("MediaFactory");
            if (mfactory == null) return;
            IMusicMedia realMedia = mfactory.CreateMusic();
            mediaInfo.Infos = media;
            mediaInfo.SetInfo(realMedia);
            MusicList.Add(realMedia);
        }
        private void AddVideoMedia(XElement media)
        {
            InfoMediaFromXml mediaInfo = new InfoMediaFromXml();
            IMediaFactory mfactory = ModuleManager.GetInstanceOf<IMediaFactory>("MediaFactory");
            if (mfactory == null) return;
            IVideoMedia realMedia = ModuleManager.GetInstanceOf<IMediaFactory>("MediaFactory").CreateVideo();
            mediaInfo.Infos = media;
            mediaInfo.SetInfo(realMedia);
            VideoList.Add(realMedia);
        }
        private void AddImageMedia(XElement media)
        {
            InfoMediaFromXml mediaInfo = new InfoMediaFromXml();
            IMediaFactory mfactory = ModuleManager.GetInstanceOf<IMediaFactory>("MediaFactory");
            if (mfactory == null) return;
            IImageMedia realMedia = ModuleManager.GetInstanceOf<IMediaFactory>("MediaFactory").CreateImage();
            mediaInfo.Infos = media;
            mediaInfo.SetInfo(realMedia);
            ImageList.Add(realMedia);
        }

        private void AddPlayList(XElement param)
        {
            IPlayList plist = ModuleManager.GetInstanceOf<IPlayList>("PlayList");
            InfoMediaFromXml mediaInfo = new InfoMediaFromXml();
            if (plist == null) return;
            plist.Title = param.Element("Title").Value;
            IEnumerable<XElement> sequences = from els in param.Element("sequence").Elements("Element") select els;
            foreach (XElement media in sequences)
            {
                mediaInfo.Infos = media;
                IMedia realMedia = ModuleManager.GetInstanceOf<IMedia>("BasicMedia");
                mediaInfo.SetInfo(realMedia);
                plist.Add(realMedia);
            }
            PlayListList.Add(plist);
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
               // if (property.PropertyType.IsSerializable == false)
                //    continue;
                XmlSerializer serializer = new XmlSerializer(property.PropertyType);
                XElement CurrentElement = serializer.SerializeAsXElement(value);
                CurrentElement.SetAttributeValue("Property", property.Name);
                e.Add(CurrentElement);
            }
        }

        public void Save<T>(T media, string serviceName) where T : IMedia
        {
            XElement medias = _root;
            XElement Element = new XElement("media");
            _save<IMedia>(media, Element);
            _save<T>(media, Element);
            Element.SetAttributeValue("type", serviceName);
            medias.Add(Element);
        }

        public void Save(IPlayList plist)
        {
            XElement file = _root;
            XElement Element = new XElement("media");
            Element.SetAttributeValue("type", "playList");
            Element.Add(new XElement("Title", plist.Title));
            Element.Add(new XElement("sequence"));
            foreach (IMedia media in plist.Collection)
            {
                XElement element = new XElement("Element");
                XElement seqElement = Element.Element("sequence");
                _save<IMedia>(media, element);
                seqElement.Add(element);
            }
            file.Add(Element);
        }

        public void Save()
        {
            _root.Save("data.xml");
        }
        #endregion
    }
}
