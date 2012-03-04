using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Utils;

namespace MWMP.ViewModels
{
    abstract class APlayer : BindableObject
    {
        public virtual RelayCommand Stop { get; protected set; }
        public virtual RelayCommand Play { get; protected set; }
        public virtual RelayCommand Speaker { get; protected set; }
        public virtual RelayCommand goTo { get; protected set; }
        public virtual RelayCommand Open { get; protected set; }
    }
}
