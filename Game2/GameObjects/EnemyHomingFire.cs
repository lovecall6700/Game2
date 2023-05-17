using Game2.Utilities;
using Microsoft.Xna.Framework;

namespace Game2.GameObjects
{
    public class EnemyHomingFire : Enemy
    {
        private readonly Timer _hormingTimer = new Timer();
        private Vector2 _target;

        public EnemyHomingFire(Game2 game2, float x, float y) : base(game2, x, y)
        {
            RImg.ClearAndAddImage(Game2.Textures.GetTexture("EnemyFireR1"));
            RImg.AddImage(Game2.Textures.GetTexture("EnemyFireR2"));
            LImg.ClearAndAddImage(Game2.Textures.GetTexture("EnemyFireL1"));
            LImg.AddImage(Game2.Textures.GetTexture("EnemyFireL2"));
            DeadImg = Game2.Textures.GetTexture("EnemyFireDead");
            Velocity = Vector2.Zero;
            MaxSpeedX = 3;
            AnimationAlways = true;
            LifeTime = 900;
        }

        public override bool MoveLeftOrRight()
        {
            Player p = Game2.PlaySc.Player;

            if (!_hormingTimer.Update())
            {
                _hormingTimer.Start(60);
                _target = p.Position;
            }

            Utility.Homing(this, _target, ref Velocity, MaxSpeedX);
            Position += Velocity;
            Rectangle.Location = Position.ToPoint();
            return false;
        }

        public override bool JumpAndGravity()
        {
            return false;
        }
    }
}