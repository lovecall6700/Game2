namespace Game2.Utilities
{
    /// <summary>
    /// フレーム数の管理
    /// </summary>
    public class Timer
    {
        /// <summary>
        /// フレーム数
        /// </summary>
        private int _time = 0;

        /// <summary>
        /// フレーム数が動作しているか(終了しているか)
        /// </summary>
        public bool Running = false;

        /// <summary>
        /// フレーム数を開始する
        /// 時間指定がゼロ以下の時はフレーム数を開始しない。
        /// </summary>
        /// <param name="count">時間</param>
        public void Start(int count)
        {
            if (count <= 0f)
            {
                _time = 0;
                Running = false;
                return;
            }

            _time = count;
            Running = true;
        }

        /// <summary>
        /// フレーム数を更新する
        /// </summary>
        /// <param name="count">カウント数</param>
        /// <returns>タイムアップしたか</returns>
        public virtual bool Update(int count = 1)
        {
            if (Running && 0f < _time)
            {
                _time -= count;
                Running = 0f < _time;
            }

            return Running;
        }

        /// <summary>
        /// 秒単位で時間を取得する。
        /// </summary>
        /// <returns>時間</returns>
        public int GetSecond()
        {
            return (int)(_time * 0.03333333f);
        }
    }
}
