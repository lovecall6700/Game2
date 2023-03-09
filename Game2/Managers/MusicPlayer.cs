using Game2.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Game2.Managers
{
    /// <summary>
    /// 音楽プレーヤー
    /// Vistaでは何故か動作しない？
    /// </summary>
    public class MusicPlayer
    {

        private TimeSpan _playPosition;

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
        private float _BGMVolume = 0.75f;

        /// <summary>
        /// SE音量
        /// </summary>
        private float _SEVolume = 0.75f;

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
        public void LoadSoundVolume()
        {
            FileStream fs = null;

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                fs = new FileStream(Path.Combine(Utility.GetSaveFilePath(), "soundvolume.dat"), FileMode.Open);
                SoundVolumeData sd = (SoundVolumeData)formatter.Deserialize(fs);
                _BGMVolume = sd.BGMVolume;
                _SEVolume = sd.SEVolume;
                SetSongVolume(_BGMVolume);
                SetSEVolume(_SEVolume);
            }
            catch
            {
                _BGMVolume = 0.75f;
                _SEVolume = 0.75f;
                SetSongVolume(_BGMVolume);
                SetSEVolume(_SEVolume);
            }
            finally
            {
                fs?.Close();
            }
        }

        /// <summary>
        /// 音量をファイルに保存する
        /// </summary>
        public void SaveSoundVolume()
        {
            FileStream fs = null;

            try
            {
                SoundVolumeData data = new SoundVolumeData
                {
                    BGMVolume = _BGMVolume,
                    SEVolume = _SEVolume
                };

                BinaryFormatter formatter = new BinaryFormatter();
                fs = new FileStream(Path.Combine(Utility.GetSaveFilePath(), "soundvolume.dat"), FileMode.Create);
                formatter.Serialize(fs, data);
            }
            catch
            {
            }
            finally
            {
                fs?.Close();
            }
        }

        /// <summary>
        /// BGMの再生を一時停止する
        /// </summary>
        public void StopSong()
        {
            try
            {
                if (_bgm != null)
                {
                    _playPosition = MediaPlayer.PlayPosition;
                    MediaPlayer.Stop();
                }
            }
            catch { }
        }

        /// <summary>
        /// BGMの再生を一時停止位置から再スタートする
        /// </summary>
        public void ReplaySong()
        {
            try
            {
                if (_bgm != null)
                {
                    if (_playPosition > _bgm.Duration)
                    {
                        _playPosition = new TimeSpan(0, 0, 0);
                    }

                    MediaPlayer.Play(_bgm, _playPosition);
                }
            }
            catch { }
        }

        /// <summary>
        /// BGMを再生する
        /// </summary>
        /// <param name="name">BGM名</param>
        public void PlaySong(string name)
        {
            //nullか同じ名前が演奏されているなら何もしない
            if (name == null || _lastSongName == name)
            {
                return;
            }

            try
            {
                _bgm = _content.Load<Song>(name);
                _lastSongName = name;

                if (_bgm != null)
                {
                    MediaPlayer.Play(_bgm);
                }
            }
            catch { }
        }

        /// <summary>
        /// 効果音を鳴らす
        /// </summary>
        /// <param name="name">効果音名</param>
        public void PlaySE(string name)
        {
            if (name == null)
            {
                return;
            }

            try
            {
                //演奏周りは不思議なことが起こるのでtry内部に入れておく
                if (_soundEffects.ContainsKey(name))
                {
                    _ = _soundEffects[name].Play();
                    return;
                }

                SoundEffect se = _content.Load<SoundEffect>(name);

                if (se != null)
                {
                    _ = se.Play();
                    _soundEffects.Add(name, se);
                }
            }
            catch { }
        }

        /// <summary>
        /// BGMの音量を設定する
        /// </summary>
        /// <param name="v">0.0から1.0までの数</param>
        public void SetSongVolume(float v)
        {
            try
            {
                _BGMVolume = MathHelper.Clamp(v, 0f, 1f);
                MediaPlayer.Volume = _BGMVolume;
            }
            catch { }
        }

        /// <summary>
        /// SEの音量を設定する
        /// </summary>
        /// <param name="v">0.0から1.0までの数</param>
        public void SetSEVolume(float v)
        {
            try
            {
                _SEVolume = MathHelper.Clamp(v, 0f, 1f);
                SoundEffect.MasterVolume = _SEVolume;
            }
            catch { }
        }

        /// <summary>
        /// BGMの音量を取得する
        /// </summary>
        /// <returns>0.0から1.0までの数</returns>
        public float GetSongVolume()
        {
            return _BGMVolume;
        }

        /// <summary>
        /// SEの音量を取得する
        /// </summary>
        /// <returns>0.0から1.0までの数</returns>
        public float GetSEVolume()
        {
            return _SEVolume;
        }
    }
}
