using Game2.Utilities;
using Microsoft.Xna.Framework;

namespace Game2.GameObjects
{
    internal class EnemyHomingFire : Enemy
    {
        private readonly Timer _hormingTimer = new Timer();
        private Vector2 _target;

        internal EnemyHomingFire(Game2 game2, float x, float y) : base(game2, x, y)
        {
            RImg.ClearAndAddImage(Game2.Textures.GetTexture("Images/EnemyFireR1"));
            RImg.AddImage(Game2.Textures.GetTexture("Images/EnemyFireR2"));
            LImg.ClearAndAddImage(Game2.Textures.GetTexture("Images/EnemyFireL1"));
            LImg.AddImage(Game2.Textures.GetTexture("Images/EnemyFireL2"));
            DeadImg = Game2.Textures.GetTexture("Images/EnemyFireDead");
            Velocity = Vector2.Zero;
            MaxSpeedX = 3;
            AnimationAlways = true;
            LifeTime = 30000f;
        }

        internal override bool MoveLeftOrRight(ref GameTime gameTime)
        {
            Player p = Game2.PlaySc.Player;

            if (!_hormingTimer.Update(ref gameTime))
            {
                _hormingTimer.Start(2000f, true);
                _target = p.Position;
            }

            if (p.Position != Position)
            {
                Velocity = Vector2.Normalize(_target - Position) * MaxSpeedX;

                if (Velocity.X > 0)
                {
                    ControlDirectionX = 1;
                }
                else
                {
                    ControlDirectionX = -1;
                }
            }

            Position += Velocity;
            Rectangle.Location = Position.ToPoint();
            return false;
        }

        internal override bool JumpAndGravity(ref GameTime gameTime)
        {
            return false;
        }
    }
}