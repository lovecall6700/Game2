using Microsoft.Xna.Framework;

namespace Game2.Managers
{
    /// <summary>
    /// 制限時間管理
    /// </summary>
    public class TimeLimitDisplay : DigitalDisplay
    {
        public TimeLimitDisplay(Game2 game2) : base(game2)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            Position = new Vector2(110, 5);
            Format = "{0:000}";
        }
    }
}
