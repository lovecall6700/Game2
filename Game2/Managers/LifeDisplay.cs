using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Managers
{
    /// <summary>
    /// ライフを表示する
    /// </summary>
    public class LifeDisplay : DigitalDisplay
    {
        public LifeDisplay(Game2 game2, GraphicsDevice device) : base(game2, device)
        {
            Format = "LIFE {0:0}";
        }

        public override void Initialize(GraphicsDevice device)
        {
            base.Initialize(device);
            Position = new Vector2(2, 20);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int life = Game2.PlaySc.Player.Life;
            Value = life < 0 ? 0 : life;
            base.Draw(spriteBatch);
        }
    }
}
