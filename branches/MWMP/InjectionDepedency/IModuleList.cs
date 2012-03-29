using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWMP.InjectionDepedency
{
    public interface IModuleList
    {
        IList<IModule> List { get; }
    }
}
