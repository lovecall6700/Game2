using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Managers
{
    /// <summary>
    /// ポーズ表示
    /// </summary>
    internal class PauseDisplay : DigitalDisplay
    {
        internal PauseDisplay(Game2 game2, SpriteFont font, GraphicsDevice device) : base(game2, font, device)
        {
            Initialize(device);
        }

        internal override void Initialize(GraphicsDevice device)
        {
            base.Initialize(device);
            Position = new Vector2(100, 128);
            Format = "PAUSE";
        }
    }
}
