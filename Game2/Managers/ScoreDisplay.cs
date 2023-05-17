using Microsoft.Xna.Framework;

namespace Game2.Managers
{
    /// <summary>
    /// スコア管理
    /// </summary>
    public class ScoreDisplay : DigitalDisplay
    {
        public ScoreDisplay(Game2 game2) : base(game2)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            Position = new Vector2(155, 5);
            Format = "  {0:00000000}";
            Value = Game2.Session.Score;
        }

        public override void Update(GameTime gameTime)
        {
            Value = Game2.Session.Score;
            base.Update(gameTime);
        }
    }
}
