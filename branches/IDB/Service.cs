using System;
using System.Reflection;

namespace IDB
{
    class Service
    {
        public Service(string name, string file, string iname, string cname)
        {
            this.name = name;
            this.file = file;
            this.iname = iname;
            this.cname = cname;
            this.moduleAssembly = Assembly.LoadFile(File);
            Type[] types = moduleAssembly.GetTypes();
            foreach (Type type in types)
            {
                if (type.GetInterface(IName) != null)
                {
                    this.constructor = type.GetConstructor(new Type[0]);
                    if (Constructor != null)
                        break;
                }
            }
        }
        private string name, file, iname, cname;
        private ConstructorInfo constructor;
        private Assembly moduleAssembly;
        public string Name
        {
            get
            {
                return this.name;
            }
        }
        public string File
        {
            get
            {
                return this.file;
            }
        }
        public string IName
        {
            get
            {
                return this.iname;
            }
        }
        public string CName
        {
            get
            {
                return this.cname;
            }
        }
        public ConstructorInfo Constructor
        {
            get
            {
                return this.constructor;
            }
        }
        public Assembly ModuleAssembly
        {
            get
            {
                return this.moduleAssembly;
            }
        }
    }
}
