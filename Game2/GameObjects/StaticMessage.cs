using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.GameObjects
{
    /// <summary>
    /// ゲーム画面内に表示するメッセージ
    /// </summary>
    internal class StaticMessage : PhysicsObject
    {
        private readonly string _message;

        internal StaticMessage(Game2 game2, float x, float y, string msg) : base(game2, x, y)
        {
            _message = msg;
        }

        internal override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game2.Font, _message, Position, Color.White);
        }
    }
}
