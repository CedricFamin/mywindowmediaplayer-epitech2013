using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.InjectionDepedency;

namespace Medias
{
    class ModulesDefinition : IModuleList
    {
        public ModulesDefinition()
        {
            List = new List<IModule>();
            List.Add(new GenericModule<MediaFactory>("MediaFactory", true));
            List.Add(new GenericModule<InfoMedia>("InfoMedia"));
            List.Add(new GenericModule<MusicMedia>("MusicMedia"));
            List.Add(new GenericModule<VideoMedia>("VideoMedia"));
        }

        public IList<IModule> List { get; private set; }
    }
}
