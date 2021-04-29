using Game2.Utilities;
using Microsoft.Xna.Framework;

namespace Game2.GameObjects
{
    internal class EnemyUFO2 : Enemy
    {
        private int _state = 0;
        private readonly Timer _shotTimer = new Timer();

        internal EnemyUFO2(Game2 game2, float x, float y) : base(game2, x, y)
        {
            Img = Game2.Textures.GetTexture("EnemyUFO");
            SetSize(256, 80);
            Attack = 255;
            Life = 14;
            UseLifeTime = false;
            UseAnimation = false;
            ShotTime = 2000f;
        }

        internal override void Update(ref GameTime gameTime)
        {
            AttackPlayer();
            RecoveryDamage(ref gameTime);

            if (ObjectStatus == PhysicsObjectStatus.Dead)
            {
                OnlyGravity();
                return;
            }

            if (!_shotTimer.Update(ref gameTime))
            {
                Shot();
                _shotTimer.Start(ShotTime, true);
            }

            if (_state == 0)
            {
                Position.Y += 10f;

                if (Position.Y > 50)
                {
                    _state = 1;
                }
            }
            else if (_state == 1)
            {
                Position.Y += 10f;

                if (Position.Y > 206)
                {
                    _state = 2;
                }
            }
            else if (_state == 2)
            {
                Position.Y -= 10f;

                if (Position.Y < -50)
                {
                    _state = 1;
                }
            }

            Rectangle.Location = Position.ToPoint();
        }

        internal override void Shot()
        {
            Game2.PlaySc.PhysicsObjs.Add(new EnemyBullet(Game2, Position.X, Position.Y + 60, 270f));
            Game2.MusicPlayer.PlaySE("SoundEffects/EnemyShot");
            ShotTime = MathHelper.Clamp(ShotTime - 200f, 100f, float.MaxValue);
        }

        internal override void Removed()
        {
            Game2.Scheduler.SetSchedule(Schedule.Ending);
        }
    }
}