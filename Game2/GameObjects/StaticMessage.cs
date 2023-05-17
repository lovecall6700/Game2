using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.GameObjects
{
    /// <summary>
    /// ゲーム画面内に表示するメッセージ
    /// </summary>
    public class StaticMessage : PhysicsObject
    {
        private readonly string _message;

        public StaticMessage(Game2 game2, float x, float y, string msg) : base(game2, x, y)
        {
            _message = msg;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game2.Font, _message, Position, Color.White);
        }
    }
}
