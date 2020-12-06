using Game2.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Managers
{
    /// <summary>
    /// ポーズ表示
    /// </summary>
    internal class PauseDisplay : DigitalDisplay
    {
        private readonly Timer _blinkTimer = new Timer();
        private bool _blink = true;

        internal PauseDisplay(Game2 game2, SpriteFont font, GraphicsDevice device) : base(game2, font, device)
        {
            Initialize(device);
            _blinkTimer.Start(500f, true);
        }

        internal override void Update(ref GameTime gameTime)
        {
            if (!_blinkTimer.Update(ref gameTime))
            {
                _blink = !_blink;
                _blinkTimer.Start(500f, true);
            }

            base.Update(ref gameTime);
        }

        internal override void Draw(ref SpriteBatch spriteBatch)
        {
            if (_blink)
            {
                base.Draw(ref spriteBatch);
            }
        }

        internal override void Initialize(GraphicsDevice device)
        {
            base.Initialize(device);
            Position = new Vector2(128, 140) - Utility.GetMsgSize(Font, "PAUSE", 1f) / 2;
            Format = "PAUSE";
        }
    }
}
