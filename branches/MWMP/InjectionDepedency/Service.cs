using System;
using System.Reflection;

namespace MWMP
{
    public class Service
    {
        #region Ctor
        public Service(string name, bool unique, Type type)
        {
            Name = name;
            Unique = unique;
            CName = type.Name;
            Constructor = type.GetConstructor(new Type[0]);
        }

        public Service(string name, string file, string cname, string unique)
        {
            bool un;
            Assembly moduleAssembly;

            Name = name;
            File = file;
            CName = cname;
            Unique = (bool.TryParse(unique, out un)) ? un : false;
            moduleAssembly = Assembly.LoadFrom(File);
            Type[] types = moduleAssembly.GetTypes();
            Instance = null;
            foreach (Type type in types)
            {
                if (type.Name == CName)
                    Constructor = type.GetConstructor(new Type[0]);
            }
        }

        #endregion /// Ctor

        #region Property
        public string Name { get; private set; }
        public string File { get; private set; }
        public string CName  { get; private set; }
        public bool Unique { get; private set; }
        private object Instance { get; set; }
        private ConstructorInfo Constructor { get; set; }
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
