using Microsoft.Xna.Framework;

namespace Game2.Utilities
{
    /// <summary>
    /// タイマーの管理
    /// </summary>
    internal class Timer
    {
        /// <summary>
        /// タイマー
        /// </summary>
        private float _time = 0f;

        /// <summary>
        /// タイマーが動作しているか(終了しているか)
        /// </summary>
        internal bool Running = false;

        /// <summary>
        /// タイマーを開始する
        /// </summary>
        /// <param name="time">時間</param>
        /// <param name="running">動作状態</param>
        internal void Start(float time, bool running)
        {
            _time = time;
            Running = running;
        }

        /// <summary>
        /// ミリ秒単位で時間を取得する。
        /// ゼロ秒以下はゼロを返す。
        /// </summary>
        /// <returns>時間</returns>
        internal float GetTime()
        {
            return _time > 0f ? _time : 0f;
        }

        /// <summary>
        /// 秒単位で時間を取得する。
        /// </summary>
        /// <returns>時間</returns>
        internal int GetSecond()
        {
            return (int)(_time / 1000);
        }

        /// <summary>
        /// ゲーム時間でタイマーを更新する
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        /// <returns>タイマーが動作しているか(終了しているか)</returns>
        internal bool Update(GameTime gameTime)
        {
            if (Running && _time > 0f)
            {
                _time -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                Running = _time > 0f;
            }

            return Running;
        }

        /// <summary>
        /// ミリ秒指定でタイマーを更新する
        /// </summary>
        /// <param name="time">ミリ秒</param>
        internal void Update(float time)
        {
            if (Running && _time > 0f)
            {
                _time -= time;
                Running = _time > 0f;
            }
        }
    }
}
