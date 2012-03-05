using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Models;
using FMOD;

namespace MusicPlayer
{
    public class MusicPlayer : IPlayer
    {
        public delegate void EndMusicEventHandler(object sender, EventArgs e);

        public MusicPlayer()
        {
            RESULT result;

            result = Factory.System_Create(ref system);
            ErrCheck(result);
            uint version = 0;
            result = system.getVersion(ref version);
            ErrCheck(result);
            if (version < VERSION.number)
                throw new ApplicationException("Error! You are using an old version of FMOD " + version.ToString("X") + ". This program requires " + VERSION.number.ToString("X") + ".");
            result = system.init(32, INITFLAGS.NORMAL, (IntPtr)null);
            ErrCheck(result);
            channelCallback = new CHANNEL_CALLBACK(OnEndMusic);
        }

        public void Deinitialize()
        {
            if (music != null)
                music.release();
            system.release();
        }

        private void ErrCheck(RESULT result)
        {
            if (result != RESULT.OK)
                throw new ApplicationException("FMOD error! " + result + " - " + Error.String(result));
        }

        public void Play()
        {
            PlayMusic(currentMusicPath);
        }

        public void PlayMusic(string path)
        {
            PlayMusic(path, false);
        }

        public void PlayMusic(string path, bool paused)
        {
            bool isPlaying = false;
            RESULT result;

            if (musicChannel != null)
                result = musicChannel.isPlaying(ref isPlaying);
            else
                isPlaying = false;
            if (currentMusicPath == path && isPlaying)
                return;
            else if (currentMusicPath == path && music != null)
            {
                result = system.playSound(CHANNELINDEX.FREE, music, false, ref musicChannel);
                ErrCheck(result);
                musicChannel.setCallback(channelCallback);
            }
        }

        public void Stop()
        {
            if (musicChannel != null)
            {
                RESULT result = musicChannel.stop();
                musicChannel = null;
                ErrCheck(result);
            }
        }

        public void Pause()
        {
            bool paused = false;
            if (musicChannel != null)
            {
                RESULT result = musicChannel.getPaused(ref paused);
                ErrCheck(result);
                result = musicChannel.setPaused(!paused);
                ErrCheck(result);
            }
        }

        public RESULT OnEndMusic(IntPtr channelraw, FMOD.CHANNEL_CALLBACKTYPE type, IntPtr commanddata1, IntPtr commanddata2)
        {
            if (EndMusic != null)
                EndMusic(currentMusicPath, new EventArgs());
            musicChannel = null;
            return RESULT.OK;
        }

        public void Update()
        {
            system.update();
        }

        private FMOD.System system = null;
        private Sound music = null;
        public string currentMusicPath;
        private Channel musicChannel = null;
        private CHANNEL_CALLBACK channelCallback;
        public event EndMusicEventHandler EndMusic;

        public void GoTo(int time)
        {
            throw new NotImplementedException();
        }

        public void Volume(int volume)
        {
            throw new NotImplementedException();
        }

        public void Mute()
        {
            throw new NotImplementedException();
        }


        public void Load(string path)
        {
            RESULT result;
            if (music != null)
            {
                result = music.release();
                ErrCheck(result);
            }
            result = system.createSound(path, MODE.SOFTWARE | MODE.CREATECOMPRESSEDSAMPLE | MODE.LOOP_OFF, ref music);
            ErrCheck(result);
            currentMusicPath = path;
        }
    }
}
