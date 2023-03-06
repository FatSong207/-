using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace Yuebon.Commons.Device
{
    /// <summary>
    ///聲音文件播放操作輔助類。除了MP3聲音文件外，還可以播放WAV格式、midi格式聲音文件。
    /// </summary>
    public class SoundPlayerHelper
    {
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);

        [DllImport("winmm.dll")]
        private static extern int waveOutGetVolume(IntPtr hwo, out uint dwVolume);

        [DllImport("winmm.dll")]
        private static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

        /// <summary>
        /// 播放聲音文件
        /// </summary>
        /// <param name="soundFileName">聲音文件路徑（可以是MP3、WAV、Midi等聲音文件）</param>
        /// <param name="Repeat">是否重復播放</param>
        public static void Play(string soundFileName,bool Repeat)
        {
            mciSendString("open \"" + soundFileName + "\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
            mciSendString("play MediaFile" + (Repeat ? " repeat" :String.Empty), null, 0, IntPtr.Zero);
        }

        /// <summary>
        /// 播放聲音嵌入資源字節數組
        /// </summary>
        /// <param name="soundEmbeddedResource">聲音文件嵌入資源字節數組（可以是MP3、WAV、Midi等聲音格式）</param>
        /// <param name="Repeat">是否重復播放</param>
        public static void Play(byte[] soundEmbeddedResource, bool Repeat)
        {
            extractResource(soundEmbeddedResource, Path.GetTempPath() + "resource.tmp");
            mciSendString("open \"" + Path.GetTempPath() + "resource.tmp" + "\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
            mciSendString("play MediaFile" + (Repeat ? " repeat" : String.Empty), null, 0, IntPtr.Zero);
        }

        /// <summary>
        /// 暫停播放
        /// </summary>
        public static void Pause()
        {
            mciSendString("stop MediaFile", null, 0, IntPtr.Zero);
        }

        /// <summary>
        /// 停止播放
        /// </summary>
        public static void Stop()
        {
            mciSendString("close MediaFile", null, 0, IntPtr.Zero);
        }

        /// <summary>
        /// 釋放資源
        /// </summary>
        public static void Dispose()
        {
            mciSendString("close all", null, 0, IntPtr.Zero);
            mciSendString("clear all", null, 0, IntPtr.Zero);
        }

        /// <summary>
        /// 獲取或設置音量的百分比
        /// </summary>
        /// <returns></returns>
        public static float VolumePercent
        {
            get
            {
                float currentVolume = (float)Math.Round(GetVolume() * 100, 0);
                return currentVolume;
            }
            set
            {
                SetVolume((float)Math.Round(value, 0) / 100);
            }
        }

        /// <summary>
        /// 獲取音量
        /// </summary>
        /// <returns></returns>
        public static float GetVolume()
        {
            uint curVol = 0;
            waveOutGetVolume(IntPtr.Zero, out curVol);
            ushort calcVol = (ushort)(curVol & 0xffff);
            float currentVolume = (float)calcVol / ushort.MaxValue;
            return currentVolume;
        }
        
        /// <summary>
        /// 設置音量
        /// </summary>
        /// <param name="volume"></param>
        public static void SetVolume(float volume)
        {
            volume = ushort.MaxValue * volume;
            uint volumeBothChannels = (((uint)volume & 0xffff) | ((uint)volume << 16));
            waveOutSetVolume(IntPtr.Zero, volumeBothChannels);
        }

        /// <SUMMARY>
        /// 返回當前狀態播放：播放，暫停，停止等
        /// </SUMMARY>
        public static string Status
        {
            get
            {
                int i = 128;
                StringBuilder stringBuilder = new StringBuilder(i);
                mciSendString("status MediaFile mode", stringBuilder, i, IntPtr.Zero);
                return stringBuilder.ToString();
            }
        }

        private static void extractResource(byte[] res,string filePath)
        {
            FileStream fs;
            BinaryWriter bw;

            if (!File.Exists(filePath))
            {
                fs = new FileStream(filePath, FileMode.OpenOrCreate);
                bw = new BinaryWriter(fs);

                foreach (byte b in res)
                    bw.Write(b);

                bw.Close();
                fs.Close();
            }
        }
    }
}
