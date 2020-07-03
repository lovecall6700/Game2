using Game2.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Game2.Managers
{
    /// <summary>
    /// 音楽プレーヤー
    /// </summary>
    internal class MusicPlayer
    {
        private readonly ContentManager _content;

        /// <summary>
        /// 効果音
        /// </summary>
        private readonly Dictionary<string, SoundEffect> _soundEffects = new Dictionary<string, SoundEffect>();

        /// <summary>
        /// BGM
        /// </summary>
        private Song _bgm = null;

        /// <summary>
        /// 最後に再生したBGM名
        /// </summary>
        private string _lastSongName = null;

        /// <summary>
        /// BGM音量
        /// </summary>
        private float _BGMVolume = 0.8f;

        /// <summary>
        /// SE音量
        /// </summary>
        private float _SEVolume = 0.8f;

        public MusicPlayer(ContentManager content)
        {
            _content = content;

            try
            {
                MediaPlayer.IsRepeating = true;
            }
            catch { }

            LoadSoundVolume();
        }

        /// <summary>
        /// 前回音量をファイルから復元する
        /// </summary>
        internal void LoadSoundVolume()
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream fs = new FileStream($@"{Utility.GetSaveFilePath()}\soundvolume.dat", FileMode.Open);
                SoundVolumeData sd = (SoundVolumeData)formatter.Deserialize(fs);
                fs.Close();
                _BGMVolume = sd.BGMVolume;
                _SEVolume = sd.SEVolume;
                SetSongVolume(_BGMVolume);
                SetSEVolume(_SEVolume);
                return;
            }
            catch
            {
            }

            _BGMVolume = 0.8f;
            _SEVolume = 0.8f;
            SetSongVolume(_BGMVolume);
            SetSEVolume(_SEVolume);
        }

        /// <summary>
        /// 音量をファイルに保存する
        /// </summary>
        internal void SaveSoundVolume()
        {
            try
            {
                SoundVolumeData data = new SoundVolumeData
                {
                    BGMVolume = _BGMVolume,
                    SEVolume = _SEVolume
                };

                BinaryFormatter formatter = new BinaryFormatter();
                FileStream fs = new FileStream($@"{Utility.GetSaveFilePath()}\soundvolume.dat", FileMode.Create);
                formatter.Serialize(fs, data);
                fs.Close();
            }
            catch
            {
            }
        }

        /// <summary>
        /// 効果音を読み込む
        /// </summary>
        /// <param name="name">効果音名</param>
        private void LoadSE(string name)
        {
            if (!_soundEffects.ContainsKey(name))
            {
                _soundEffects.Add(name, _content.Load<SoundEffect>(name));
            }
        }

        internal void StopSong()
        {
            try
            {
                MediaPlayer.Stop();
            }
            catch { }
        }

        internal void ReplaySong()
        {
            try
            {
                if (_bgm != null)
                {
                    MediaPlayer.Play(_bgm);
                }
            }
            catch { }
        }

        /// <summary>
        /// 効果音を破棄する
        /// </summary>
        /// <param name="name">効果音名</param>
        internal void UnLoadSE(string name)
        {
            if (_soundEffects.ContainsKey(name))
            {
                SoundEffect s = _soundEffects[name];
                s.Dispose();
                _soundEffects.Remove(name);
            }
        }

        /// <summary>
        /// BGMを再生する
        /// </summary>
        /// <param name="name">BGM名</param>
        internal void PlaySong(string name)
        {
            if (name == null)
            {
                return;
            }

            try
            {
                //同じ名前が演奏されていない場合のみ、新たに演奏を開始
                if (_lastSongName != name)
                {
                    _bgm = _content.Load<Song>(name);
                    MediaPlayer.Play(_bgm);
                    _lastSongName = name;
                }
            }
            catch { }
        }

        /// <summary>
        /// 効果音を鳴らす
        /// </summary>
        /// <param name="name">効果音名</param>
        internal void PlaySE(string name)
        {
            if (name == null)
            {
                return;
            }

            try
            {
                LoadSE(name);
                _soundEffects[name].Play();
            }
            catch { }
        }

        internal void SetSongVolume(float v)
        {
            try
            {
                _BGMVolume = MathHelper.Clamp(v, 0f, 1f);
                MediaPlayer.Volume = _BGMVolume;
            }
            catch { }
        }

        internal void SetSEVolume(float v)
        {
            try
            {
                _SEVolume = MathHelper.Clamp(v, 0f, 1f);
                SoundEffect.MasterVolume = _SEVolume;
            }
            catch { }
        }
    }
}
