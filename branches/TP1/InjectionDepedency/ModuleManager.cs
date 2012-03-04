using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace IDB
{
    /// <summary>
    /// Manage the loading file and the service creator
    /// </summary>
    class ModuleManager
    {
        #region Static fields
        static private ModuleManager instance;
        #endregion

        #region Fields
        private Dictionary<string, Service> services;
        #endregion /// Fields

        #region Singletron
        static public ModuleManager getInstance()
        {
            if (ModuleManager.instance == null)
                ModuleManager.instance = new ModuleManager();
            return ModuleManager.instance;
        }

        static public void deleteInstance()
        {
            ModuleManager.instance = null;
        }

        #endregion /// Singleton

        #region Ctor
        /// <summary>
        /// Default constructor
        /// </summary>
        private ModuleManager()
        {
            this.services = new Dictionary<string, Service>();
        }

        /// <summary>
        /// Constructor of ModuleManager
        /// </summary>
        /// <param name="filename">path of XML file</param>
        private ModuleManager(string filename)
        {
            this.services = new Dictionary<string, Service>();
            this.load(filename);
        }
        #endregion /// Ctor

        #region Methods
        /// <summary>
        /// Add services with an XML file
        /// </summary>
        /// <param name="filename"></param>
        public void load(string filename)
        {
            XElement xml = XElement.Load(filename);
            IEnumerable<XElement> services = xml.Elements("service");
            foreach (XElement e in services)
            {
                Service s;
                try
                {
                    s = new Service(e.Element("name").Value, e.Element("file").Value,
                                    e.Element("class").Value, e.Element("interface").Value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    continue;
                }
                Console.WriteLine(s.Name + " " + s.File + " " + s.CName + " " + s.IName);
                this.services.Add(s.Name, s);
            }
        }

        /// <summary>
        /// Regist a service
        /// </summary>
        /// <param name="s"></param>
        public void addService(ref Service s)
        {
            this.services.Add(s.Name, s);
        }

        /// <summary>
        /// Create and return a service
        /// </summary>
        /// <typeparam name="T">Interface of the service</typeparam>
        /// <param name="serviceName">Name of the service</param>
        /// <returns>The service or default(T)</returns>
        public T getInstanceOf<T>(string serviceName)
        {
            Service s;
            if (this.services.TryGetValue(serviceName, out s))
            {
                return (T)s.Constructor.Invoke(new Object[0]);
            }
            return default(T);
        }
        #endregion /// Methods
    }
}
