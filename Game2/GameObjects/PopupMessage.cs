using Game2.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.GameObjects
{
    /// <summary>
    /// 宝箱を開けた場合などゲーム画面内にちょっと表示するメッセージ
    /// </summary>
    internal class PopupMessage : PhysicsObject
    {
        private readonly SpriteFont _font;
        private readonly string _message;
        private readonly Timer _timer = new Timer();

        internal PopupMessage(Game2 game2, float x, float y, string msg, SpriteFont font) : base(game2, x, y)
        {
            _message = msg;
            _font = font;
            _timer.Start(1000f, true);
        }

        internal override void Draw(ref Vector2 offset, ref GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, _message, Position - offset, Color.White);
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
