using Microsoft.Xna.Framework;

namespace Game2.Managers
{
    /// <summary>
    /// スコア管理
    /// </summary>
    public class HighScoreDisplay : DigitalDisplay
    {
        public HighScoreDisplay(Game2 game2) : base(game2)
        {
        }

        public override void Initialize()
        {
            Position = new Vector2(155, 5);
            Format = "HI{0:00000000}";
            Value = Game2.Session.HighScore;
        }
    }
}
