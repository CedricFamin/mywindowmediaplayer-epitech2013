using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Utils;
using MWMP.Models;

namespace MWMP.ViewModels
{
    class MusicPlayerViewModel : APlayer
    {
        private Player player;

        public MusicPlayerViewModel()
        {
            this.player = new Player();
            Play = new RelayCommand((param) =>
            {
                    this.player.Play();
            });
            Stop = new RelayCommand((param) => this.player.Stop());
            Pause = new RelayCommand((param) => this.player.Pause());
			Next = new RelayCommand((param) => this.player.Next());
            Open = new RelayCommand((param) => 
                {
                    IMedia media = new MusicMedia(param as string);
                    this.player.add(media);
                });
        }
        public override RelayCommand Stop { get; protected set; }
		public override RelayCommand Next { get; protected set; }
		public override RelayCommand Pause { get; protected set; }
        public override RelayCommand Play { get; protected set; }
        public override RelayCommand Speaker { get; protected set; }
        public override RelayCommand goTo { get; protected set; }
        public override RelayCommand Open { get; protected set; }
    }
}
