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

        internal PopupMessage(ref Game2 game2, ref SpriteFont font, float x, float y, string msg) : base(ref game2, ref font, x, y, msg)
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
