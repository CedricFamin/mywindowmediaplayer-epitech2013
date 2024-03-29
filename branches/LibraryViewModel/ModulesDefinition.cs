﻿using System.Collections.Generic;
using MWMP.InjectionDepedency;

namespace LibraryViewModel
{
    class ModulesDefinition : IModuleList
    {
        public ModulesDefinition()
        {
            List = new List<IModule>();
            List.Add(new GenericModule<PlayListLibrary>("PlayListLibrary", true));
            List.Add(new GenericModule<MusicLibrary>("MusicLibrary", true));
            List.Add(new GenericModule<VideoLibrary>("VideoLibrary", true));
            List.Add(new GenericModule<ImageLibrary>("ImageLibrary", true));
            List.Add(new GenericModule<GlobalLibrary>("GlobalLibrary", true));
        }

        public IList<IModule> List { get; private set; }
    }
}
