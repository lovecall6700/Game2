using Game2.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.GameObjects
{
    /// <summary>
    /// 宝箱を開けた場合などゲーム画面内にちょっと表示するメッセージ
    /// </summary>
    internal class PopupMessage : StaticMessage
    {
        private readonly Timer _timer = new Timer();

        internal PopupMessage(Game2 game2, float x, float y, string msg, SpriteFont font) : base(game2, x, y, msg, font)
        {
            _timer.Start(1000f, true);
        }

        internal override void Update(ref GameTime gameTime)
        {
            if (!_timer.Update(ref gameTime))
            {
                ObjectStatus = PhysicsObjectStatus.Remove;
            }

            Position.Y -= 2f;
        }
    }
}
