using Game2.GameObjects;
using Game2.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Managers
{
    /// <summary>
    /// 制限時間管理
    /// </summary>
    internal class TimeLimitDisplay : DigitalDisplay
    {
        /// <summary>
        /// 制限時間タイマー
        /// </summary>
        internal Timer Timer = new Timer();

        internal TimeLimitDisplay(Game2 game2, GraphicsDevice device) : base(game2, device)
        {
            Format = "{0:000}";
        }

        internal override void Initialize(GraphicsDevice device)
        {
            base.Initialize(device);
            Position = new Vector2(110, 5);
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            Value = Timer.GetSecond();
            base.Draw(spriteBatch);
        }

        internal override void Update(GameTime gameTime)
        {
            if (Game2.Inventory.HasTimeItem())
            {
                Timer.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds / 2f);
            }
            else
            {
                Timer.Update(gameTime);
            }

            if (!Timer.Running)
            {
                if (Game2.PlaySc.Player.ObjectStatus == PhysicsObjectStatus.Normal)
                {
                    Game2.PlaySc.EffectObjs.Add(new PopupMessage(Game2, Game2.PlaySc.Player.Position.X, Game2.PlaySc.Player.Position.Y, "TIME OVER"));
                    Game2.PlaySc.Player.ObjectStatus = PhysicsObjectStatus.Dead;
                    Game2.Inventory.SetItem("Shoes", false);
                    Game2.Inventory.SetItem("Time", false);
                }
            }
        }
    }
}
