using Game2.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Managers
{
    /// <summary>
    /// ポーズ表示
    /// </summary>
    public class PauseDisplay : DigitalDisplay
    {
        private readonly Timer _blinkTimer = new Timer();
        private bool _blink = true;

        public PauseDisplay(Game2 game2) : base(game2)
        {
            _blinkTimer.Start(15);
        }

        public override void Update()
        {
            if (!_blinkTimer.Update())
            {
                _blink = !_blink;
                _blinkTimer.Start(15);
            }

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_blink)
            {
                base.Draw(spriteBatch);
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            Position = new Vector2(128, 140) - (Utility.GetMsgSize(Game2.Font, "PAUSE", 1f) / 2);
            Format = "PAUSE";
        }
    }
}
