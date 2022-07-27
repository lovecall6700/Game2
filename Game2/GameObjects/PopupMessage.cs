using Game2.Utilities;
using Microsoft.Xna.Framework;

namespace Game2.GameObjects
{
    /// <summary>
    /// 宝箱を開けた場合などゲーム画面内にちょっと表示するメッセージ
    /// </summary>
    public class PopupMessage : StaticMessage
    {
        private readonly Timer _timer = new Timer();

        public PopupMessage(Game2 game2, float x, float y, string msg) : base(game2, x, y, msg)
        {
            _timer.Start(30);
        }

        public override void Update(GameTime gameTime)
        {
            if (!_timer.Update())
            {
                ObjectStatus = PhysicsObjectStatus.Remove;
            }

            Position.Y -= 2f;
        }
    }
}
