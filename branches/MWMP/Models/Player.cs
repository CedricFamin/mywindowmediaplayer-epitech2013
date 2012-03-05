using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace MWMP.Models
{
    class Player : IPlayer
    {
        #region Player Type
        private IPlayer currentPlayer;
        private IPlayer musicPlayer;
        #endregion /// Player TYpe

        #region Property
        private bool isRunning { get; set; }
        private int currentMediaPos { get; set; }
        public List<IMedia> PlayList { get; private set; }
        #endregion /// Property

        #region Ctor
        public Player()
        {
            ModuleManager.getInstance().load("./player.xml");
            musicPlayer = ModuleManager.getInstance().getInstanceOf<IPlayer>("MusicPlayer");
            PlayList = new List<IMedia>();
            isRunning = false;
            currentMediaPos = -1;
        }
        #endregion /// Ctor

        #region Methods
        public void SetPlayer(MusicMedia m)
        {
            currentPlayer = musicPlayer;
            musicPlayer.Load(m.Path);
        }

        public void add(IMedia m)
        {
            PlayList.Add(m);
            if (currentMediaPos == -1)
            {
                currentMediaPos = 0;
                PlayList[currentMediaPos].loadOnPlayer(this);
            }
        }

        public void del(int index)
        {
            if (currentMediaPos == index)
            {
                Stop();
                if (currentMediaPos > 0)
                    PlayList[currentMediaPos - 1].loadOnPlayer(this);
                else
                    currentMediaPos = -1;
            }
            else if (currentMediaPos > index)
                currentMediaPos--;
            PlayList.RemoveAt(index);
        }

        public void Next()
        {
            if (currentMediaPos == -1) return;
           	currentMediaPos++;
            if (currentMediaPos >= PlayList.Count)
                currentMediaPos = 0;
            Stop();
            PlayList[currentMediaPos].loadOnPlayer(this);
			Play();
        }

        public void Previous()
        {
            if (currentMediaPos == -1) return;
            currentMediaPos--;
            if (currentMediaPos < 0)
                currentMediaPos = PlayList.Count - 1;
            Stop();
            PlayList[currentMediaPos].loadOnPlayer(this);
        }
        #endregion /// Methods

        #region IPlayer interface
        public void Play()
        {
            currentPlayer.Play();
        }

        public void Pause()
        {
            currentPlayer.Pause();
        }

        public void Stop()
        {
            currentPlayer.Stop();
        }

        public void GoTo(int time)
        {
            currentPlayer.GoTo(time);
        }

        public void Volume(int volume)
        {
            currentPlayer.Volume(volume);
        }

        public void Mute()
        {
            currentPlayer.Mute();
        }

        public void Load(string path)
        {
            ;
        }
        #endregion
    }
}
