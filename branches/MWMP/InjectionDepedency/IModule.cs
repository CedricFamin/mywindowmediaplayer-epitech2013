using System;

namespace MWMP.InjectionDepedency
{
    public interface IModule
    {
        bool Unique { get; }
        string ModuleName { get; }
        Type ModuleType { get; }
    }
}
