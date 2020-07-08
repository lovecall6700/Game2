﻿using Game2.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        internal TimerScreen(Game2 game2, SpriteFont font) : base(game2, font)
        {
            WaitTimer.Start(500f, true);
        }

        internal override void Update(ref Vector2 offset, ref GameTime gameTime)
        {
            //画面が出てからしばらくは操作できない
            WaitTimer.Update(ref gameTime);

            if (WaitTimer.Running)
            {
                return;
            }

            Timer.Update(ref gameTime);

            if (!Timer.Running)
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