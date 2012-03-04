using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Utils;
using FMOD;

namespace MWMP.Models
{
    public delegate void EndMusicEventHandler(object sender, EventArgs e);

    class MusicPlayer
    {
        public static bool Initialize()
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
            return true;
        }

        public static void Deinitialize()
        {
            if (music != null)
                music.release();
            system.release();
        }

        private static void ErrCheck(RESULT result)
        {
            if (result != RESULT.OK)
                throw new ApplicationException("FMOD error! " + result + " - " + Error.String(result));
        }

        public static void PlayMusic()
        {
            PlayMusic(currentMusicPath);
        }

        public static void PlayMusic(string path)
        {
            PlayMusic(path, false);
        }

        public static void PlayMusic(string path, bool paused)
        {
            bool isPlaying = false;
            RESULT result;

            if (musicChannel != null)
                result = musicChannel.isPlaying(ref isPlaying);
            else
                isPlaying = false;
            if (currentMusicPath == path && isPlaying)
                return;
            else if (currentMusicPath == path)
            {
                result = system.playSound(CHANNELINDEX.FREE, music, false, ref musicChannel);
                ErrCheck(result);
            }
            else
            {
                if (music != null)
                {
                    result = music.release();
                    ErrCheck(result);
                }

                result = system.createSound(path, MODE.SOFTWARE | MODE.CREATECOMPRESSEDSAMPLE | MODE.LOOP_OFF, ref music);
                ErrCheck(result);

                result = system.playSound(CHANNELINDEX.FREE, music, paused, ref musicChannel);
                ErrCheck(result);
                musicChannel.setCallback(channelCallback);

                currentMusicPath = path;
            }
        }

        public static void StopMusic()
        {
            if (musicChannel != null)
            {
                RESULT result = musicChannel.stop();
                musicChannel = null;
                ErrCheck(result);
            }
        }

        public static void PauseMusic()
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

        public static RESULT OnEndMusic(IntPtr channelraw, FMOD.CHANNEL_CALLBACKTYPE type, IntPtr commanddata1, IntPtr commanddata2)
        {
            if (EndMusic != null)
                EndMusic(currentMusicPath, new EventArgs());
            musicChannel = null;
            return RESULT.OK;
        }

        public static void Update()
        {
            system.update();
        }

        private static FMOD.System system = null;
        private static Sound music = null;
        private static string currentMusicPath;
        private static Channel musicChannel = null;
        private static CHANNEL_CALLBACK channelCallback;
        public static event EndMusicEventHandler EndMusic;
    }
}
