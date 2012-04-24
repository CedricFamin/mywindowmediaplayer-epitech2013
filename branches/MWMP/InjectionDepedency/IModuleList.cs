using System.Collections.Generic;

namespace MWMP.InjectionDepedency
{
    public interface IModuleList
    {
        IList<IModule> List { get; }
    }
}
