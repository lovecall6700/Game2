using Game2.Utilities;
using Microsoft.Xna.Framework;

namespace Game2.GameObjects
{
    internal class EnemyHomingFire : Enemy
    {
        private readonly Timer _hormingTimer = new Timer();
        private Vector2 _target;

        internal EnemyHomingFire(ref Game2 game2, float x, float y) : base(ref game2, x, y)
        {
            RImg.ClearAndAddImage(Game2.Textures.GetTexture("EnemyFireR1"));
            RImg.AddImage(Game2.Textures.GetTexture("EnemyFireR2"));
            LImg.ClearAndAddImage(Game2.Textures.GetTexture("EnemyFireL1"));
            LImg.AddImage(Game2.Textures.GetTexture("EnemyFireL2"));
            DeadImg = Game2.Textures.GetTexture("EnemyFireDead");
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

            Utility.Homing(this, _target, ref Velocity, MaxSpeedX);
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