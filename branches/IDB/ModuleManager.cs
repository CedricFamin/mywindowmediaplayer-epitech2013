using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace IDB
{
    class ModuleManager
    {
        public ModuleManager()
        {
            this.services = new Dictionary<string, Service>();
        }

        public ModuleManager(string filename)
        {
            this.services = new Dictionary<string, Service>();
            this.load(filename);
        }

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

        public void addService(ref Service s)
        {
            this.services.Add(s.Name, s);
        }

        public T getInstanceOf<T>(string serviceName)
        {
            Service s;
            if (this.services.TryGetValue(serviceName, out s))
            {
                return (T)s.Constructor.Invoke(new Object[0]);
            }
            return default(T);
        }

        private Dictionary<string, Service> services;
    }
}
