using Game2.Utilities;
using Microsoft.Xna.Framework;

namespace Game2.GameObjects
{
    /// <summary>
    /// 宝箱を開けた場合などゲーム画面内にちょっと表示するメッセージ
    /// </summary>
    internal class PopupMessage : StaticMessage
    {
        private readonly Timer _timer = new Timer();

        internal PopupMessage(Game2 game2, float x, float y, string msg) : base(game2, x, y, msg)
        {
            _timer.Start(1000f, true);
        }

        internal override void Update(GameTime gameTime)
        {
            if (!_timer.Update(gameTime))
            {
                ObjectStatus = PhysicsObjectStatus.Remove;
            }

            Position.Y -= 2f;
        }
    }
}
