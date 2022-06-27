using Game2.Utilities;
using Microsoft.Xna.Framework;

namespace Game2.Screens
{
    /// <summary>
    /// 時間経過で遷移する画面の親
    /// </summary>
    internal class TimerScreen : MessageScreen
    {
        /// <summary>
        /// 表示時間
        /// </summary>
        internal readonly Timer Timer = new Timer();

        /// <summary>
        /// 画面が出てからしばらくは操作できない
        /// </summary>
        internal readonly Timer WaitTimer = new Timer();

        internal TimerScreen(ref Game2 game2) : base(ref game2)
        {
            WaitTimer.Start(500f, true);
        }

        internal override void Update(GameTime gameTime)
        {
            //画面が出てからしばらくは操作できない
            if (WaitTimer.Update(gameTime))
            {
                return;
            }

            if (!Timer.Update(gameTime))
            {
                Timeup();
            }
        }

        /// <summary>
        /// 時間経過後の処理
        /// </summary>
        internal virtual void Timeup()
        {
        }
    }
}
