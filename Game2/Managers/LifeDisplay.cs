using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Managers
{
    /// <summary>
    /// ライフを表示する
    /// </summary>
    internal class LifeDisplay : DigitalDisplay
    {
        internal LifeDisplay(ref Game2 game2, ref SpriteFont font, GraphicsDevice device) : base(ref game2, ref font, device)
        {
            Format = "LIFE {0:0}";
        }

        internal override void Initialize(GraphicsDevice device)
        {
            base.Initialize(device);
            Position = new Vector2(2, 20);
        }

        internal override void Draw(ref SpriteBatch spriteBatch)
        {
            int life = Game2.PlaySc.Player.Life;
            Value = life < 0 ? 0 : life;
            base.Draw(ref spriteBatch);
        }
    }
}
