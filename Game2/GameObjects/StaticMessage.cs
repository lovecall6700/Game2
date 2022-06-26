using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.GameObjects
{
    /// <summary>
    /// ゲーム画面内に表示するメッセージ
    /// </summary>
    internal class StaticMessage : PhysicsObject
    {
        private readonly SpriteFont _font;
        private readonly string _message;

        internal StaticMessage(ref Game2 game2, ref SpriteFont font, float x, float y, string msg) : base(ref game2, x, y)
        {
            _message = msg;
            _font = font;
        }

        internal override void Draw(ref GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, _message, Position, Color.White);
        }
    }
}
