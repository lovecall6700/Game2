using Microsoft.Xna.Framework;

namespace Game2.Managers
{
    /// <summary>
    /// ライフを表示する
    /// </summary>
    public class LifeDisplay : DigitalDisplay
    {
        public LifeDisplay(Game2 game2) : base(game2)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            Position = new Vector2(2, 20);
            Format = "LIFE {0:0}";
        }

        public override void Update(GameTime gameTime)
        {
            int life = Game2.PlaySc.Player.Life;
            Value = life < 0 ? 0 : life;
            base.Update(gameTime);
        }
    }
}
