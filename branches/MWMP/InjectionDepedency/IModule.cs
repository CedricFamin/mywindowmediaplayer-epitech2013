using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWMP.InjectionDepedency
{
    public interface IModule
    {
        bool Unique { get; }
        string ModuleName { get; }
        Type ModuleType { get; }
    }
}
