using Game2.Utilities;
using Microsoft.Xna.Framework;

namespace Game2.GameObjects
{
    internal class EnemyBird : Enemy
    {
        private readonly Timer _timer = new Timer();
        private bool _exit = false;

        internal EnemyBird(Game2 game2, float x, float y) : base(game2, x, y)
        {
            RImg.ClearAndAddImage(Game2.Textures.GetTexture("EnemyBirdR1"));
            RImg.AddImage(Game2.Textures.GetTexture("EnemyBirdR2"));
            LImg.ClearAndAddImage(Game2.Textures.GetTexture("EnemyBirdL1"));
            LImg.AddImage(Game2.Textures.GetTexture("EnemyBirdL2"));
            DeadImg = Game2.Textures.GetTexture("EnemyBirdDead");
            Velocity = Vector2.Zero;
            ControlDirectionX = 0;
            MaxSpeedX = 9;
            AnimationAlways = true;
            //一定時間たったら退場する
            _timer.Start(2000f, true);
        }

        internal override bool MoveLeftOrRight(ref GameTime gameTime)
        {
            Velocity.X += ControlDirectionX * AirAccelerationX;
            Velocity.X = MathHelper.Clamp(Velocity.X, -MaxSpeedX, MaxSpeedX);
            Rectangle.X = (int)(Position.X + Velocity.X);
            Position.X = Rectangle.X;
            return false;
        }

        internal override bool JumpAndGravity(ref GameTime gameTime)
        {
            if (!_timer.Update(ref gameTime) && !_exit)
            {
                _exit = true;
                Player p = Game2.PlaySc.Player;

                if (p.Position.Y < Position.Y)
                {
                    Gravity = -1;
                }
            }

            if (_exit)
            {
                OnlyGravity();
            }

            return false;
        }
    }
}