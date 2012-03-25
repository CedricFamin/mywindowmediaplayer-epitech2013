using System;
using System.Reflection;

namespace MWMP
{
    public class Service
    {
        #region Ctor
        public Service(string name, string file, string cname, string iname, string unique)
        {
            bool un;

            Name = name;
            File = file;
            IName = iname;
            CName = cname;
            Unique = (bool.TryParse(unique, out un)) ? un : false;
            ModuleAssembly = Assembly.LoadFrom(File);
            Type[] types = ModuleAssembly.GetTypes();
            Instance = null;
            foreach (Type type in types)
            {

                if (type.Name == CName)
                {
                    Constructor = type.GetConstructor(new Type[0]);
                }
            }
        }

        #endregion /// Ctor

        #region Property
        public string Name { get; private set; }
        public string File { get; private set; }
        public string IName { get; private set; }
        public string CName  { get; private set; }
        public bool Unique { get; private set; }
        private object Instance { get; set; }
        private ConstructorInfo Constructor { get; set; }
        private Assembly ModuleAssembly { get; set; }
        #endregion /// Property

        public object GetInstance()
        {
            if (Unique)
            {
                if (Instance == null)
                    Instance = Constructor.Invoke(new object[0]);
                return Instance;
            }
            return Constructor.Invoke(new object[0]);
        }
    }
}
