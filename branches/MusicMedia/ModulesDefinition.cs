﻿using System.Collections.Generic;
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
            List.Add(new GenericModule<ImageMedia>("ImageMedia"));
            List.Add(new GenericModule<MusicMedia>("MusicMedia"));
            List.Add(new GenericModule<VideoMedia>("VideoMedia"));
            List.Add(new GenericModule<PlayList>("PlayList"));
            List.Add(new GenericModule<BasicMedia>("BasicMedia"));
        }

        public IList<IModule> List { get; private set; }
    }
}
