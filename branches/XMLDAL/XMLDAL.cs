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
        public IList<IMusicMedia> MusicList { get; private set; }
        public IList<IVideoMedia> VideoList { get; private set; }
        public IList<IImageMedia> ImageList { get; private set; }
        #endregion

        #region Ctor
        public XMLDAL()
        {
            _root = new XElement("medias");
            MusicList = new List<IMusicMedia>();
            VideoList = new List<IVideoMedia>();
            ImageList = new List<IImageMedia>();
            if (File.Exists("data.xml") == false) return;
            XElement root;
            try { root = XElement.Load("data.xml"); }
            catch { return; }
            IEnumerable<XElement> medias = from els in root.Elements("media") select els;

            foreach (XElement media in medias)
            {
                switch (media.Attribute("type").Value)
                {
                    case "music":
                        this.AddMusicMedia(media);
                        break;
                    case "video":
                        this.AddVideoMedia(media);
                        break;
                    case "image":
                        this.AddImageMedia(media);
                        break;
                }
            }
        }
        #endregion
        
        #region private methods
        private void AddMusicMedia(XElement media)
        {
            InfoMediaFromXml mediaInfo = new InfoMediaFromXml();
            IMusicMedia realMedia = ModuleManager.GetInstanceOf<IMediaFactory>("MediaFactory").CreateMusic();
            mediaInfo.Infos = media;
            realMedia.SetInfo(mediaInfo);
            MusicList.Add(realMedia);
        }
        private void AddVideoMedia(XElement media)
        {
            InfoMediaFromXml mediaInfo = new InfoMediaFromXml();
            IVideoMedia realMedia = ModuleManager.GetInstanceOf<IMediaFactory>("MediaFactory").CreateVideo();
            mediaInfo.Infos = media;
            realMedia.SetInfo(mediaInfo);
            VideoList.Add(realMedia);
        }
        private void AddImageMedia(XElement media)
        {
            InfoMediaFromXml mediaInfo = new InfoMediaFromXml();
            IImageMedia realMedia = ModuleManager.GetInstanceOf<IMediaFactory>("MediaFactory").CreateImage();
            mediaInfo.Infos = media;
            realMedia.SetInfo(mediaInfo);
            ImageList.Add(realMedia);
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
