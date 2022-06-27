using Game2.Managers;
using Game2.Utilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Game2.GameObjects
{
    internal class EnemyBoss : Enemy
    {
        internal EnemyBoss RootBody;
        internal EnemyBoss UpperBody;
        internal int ID;
        internal bool Tail = false;
        private readonly List<Vector2> _history = new List<Vector2>();
        private Vector2 _lastPosition;
        private readonly Timer _hormingTimer = new Timer();
        private Vector2 _target;

        internal EnemyBoss(ref Game2 game2, float x, float y, int id) : base(ref game2, x, y)
        {
            ID = id;

            if (ID == 0)
            {
                RImg.ClearAndAddImage(Game2.Textures.GetTexture("EnemyBossR1"));
                LImg.ClearAndAddImage(Game2.Textures.GetTexture("EnemyBossL1"));
            }
            else
            {
                RImg.ClearAndAddImage(Game2.Textures.GetTexture("EnemyBossBody"));
                LImg.ClearAndAddImage(Game2.Textures.GetTexture("EnemyBossBody"));
            }

            DeadImg = Game2.Textures.GetTexture("EnemyBossBody");
            _lastPosition = Position;
            Life = 2;
            MaxSpeedX = 2;
            AnimationAlways = true;
            UseLifeTime = false;
        }

        internal void ResetAllHistory()
        {
            for (int i = 0; i < 10; i++)
            {
                _history.Add(Position);
            }
        }

        internal override bool JumpAndGravity(GameTime gameTime)
        {
            return false;
        }

        internal override bool MoveLeftOrRight(GameTime gameTime)
        {
            if (ID == 0)
            {
                Player p = Game2.PlaySc.Player;

                if (!_hormingTimer.Update(gameTime))
                {
                    _hormingTimer.Start(2000f, true);
                    _target = p.Position;
                }

                Utility.Homing(this, _target, ref Velocity, MaxSpeedX);

                if ((_lastPosition != Position) && Vector2.Distance(_lastPosition, Position) > 15f)
                {
                    _history.Insert(0, Position);
                    _lastPosition = Position;

                    if (_history.Count > 20)
                    {
                        _history.RemoveAt(_history.Count - 1);
                    }
                }

                Position += Velocity;
                Rectangle.Location = Position.ToPoint();
            }
            else
            {
                CopyPosition();
                Img = Game2.Textures.GetTexture("EnemyBossBody");
            }

            return false;
        }

        internal void CopyPosition()
        {
            if (ID == 0)
            {
                return;
            }

            Position = RootBody._history[ID - 1];
            Rectangle.Location = Position.ToPoint();
        }

        internal override void Damage(int damage)
        {
            //体の最後以外は無敵
            if (Tail)
            {
                base.Damage(damage);
            }
        }

        internal override void Died()
        {
            base.Died();

            if (ID != 0)
            {
                UpperBody.Tail = true;
            }
        }

        internal override void Removed()
        {
            if (ID == 0)
            {
                Game2.Scheduler.SetSchedule(Schedules.Ending);
            }
        }
    }
}