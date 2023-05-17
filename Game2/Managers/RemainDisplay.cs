using Microsoft.Xna.Framework;

namespace Game2.Managers
{
    /// <summary>
    /// 残機管理
    /// </summary>
    public class RemainDisplay : DigitalDisplay
    {
        public RemainDisplay(Game2 game2) : base(game2)
        {
        }

        public override void Initialize()
        {
            Position = new Vector2(2, 5);
            Format = "REMAIN {0:0}";
        }

        public override void Update()
        {
            Value = Game2.Session.Remain;
        }
    }
}
