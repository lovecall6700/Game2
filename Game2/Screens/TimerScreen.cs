using Game2.Utilities;
using Microsoft.Xna.Framework;

namespace Game2.Screens
{
    /// <summary>
    /// 時間経過で遷移する画面の親
    /// </summary>
    public class TimerScreen : MessageScreen
    {
        /// <summary>
        /// 表示時間
        /// </summary>
        public readonly Timer Timer = new Timer();

        /// <summary>
        /// 画面が出てからしばらくは操作できない
        /// </summary>
        public readonly Timer WaitTimer = new Timer();

        public TimerScreen(Game2 game2) : base(game2)
        {
            WaitTimer.Start(15);
        }

        public override void Update(GameTime gameTime)
        {
            //画面が出てからしばらくは操作できない
            if (WaitTimer.Update())
            {
                return;
            }

            if (!Timer.Update())
            {
                Timeup();
            }
        }

        /// <summary>
        /// 時間経過後の処理
        /// </summary>
        public virtual void Timeup()
        {
        }
    }
}
