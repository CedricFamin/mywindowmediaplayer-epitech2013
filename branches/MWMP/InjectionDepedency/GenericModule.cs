using System;

namespace MWMP.InjectionDepedency
{
    public class GenericModule<T> : IModule
    {
        public GenericModule(string moduleName, bool unique = false)
        {
            Unique = unique;
            ModuleName = moduleName;
        }

        public bool Unique { get; private set; }
        public string ModuleName { get; private set; }
        public Type ModuleType
        {
            get { return typeof(T); }
        }
    }
}
