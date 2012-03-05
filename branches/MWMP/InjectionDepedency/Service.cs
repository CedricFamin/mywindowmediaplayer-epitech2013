using System;
using System.Reflection;

namespace MWMP
{
    class Service
    {
        #region Ctor
        public Service(string name, string file, string cname, string iname)
        {
            Name = name;
            File = file;
            IName = iname;
            CName = cname;
            ModuleAssembly = Assembly.LoadFrom(File);
            Type[] types = ModuleAssembly.GetTypes();
            foreach (Type type in types)
            {
                if (type.GetInterface(IName) != null)
                {
                    Constructor = type.GetConstructor(new Type[0]);
                    if (Constructor != null)
                        break;
                }
            }
        }

        #endregion /// Ctor

        #region Property
        public string Name { get; private set; }
        public string File { get; private set; }
        public string IName { get; private set; }
        public string CName  { get; private set; }
        public ConstructorInfo Constructor { get; private set; }
        public Assembly ModuleAssembly { get; private set; }
        #endregion /// Property
    }
}
