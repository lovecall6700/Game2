using Microsoft.Xna.Framework;
using System;

namespace Game2.GameObjects
{
    public class EnemyUPeopleJump : EnemyUPeopleBasic
    {
        private readonly Random _rnd = new Random();
        private Rectangle _jumpRect = new Rectangle(0, 0, 8, 16);

        public EnemyUPeopleJump(Game2 game2, float x, float y) : base(game2, x, y)
        {
        }

        public override void FinallyUpdate()
        {
            if (GroundBlock != null)
            {
                _jumpRect.Y = (int)Position.Y + Height;

                if (ControlDirectionX == -1)
                {
                    _jumpRect.X = (int)Position.X - 8;
                }
                else if (ControlDirectionX == 1)
                {
                    _jumpRect.X = (int)Position.X - Width;
                }

                bool jump = true;

                foreach (GameObject o in Game2.PlaySc.NearMapObjs)
                {
                    if (o.ObjectKind == GameObjectKinds.Disable)
                    {
                        continue;
                    }
                    else if (!Rectangle.Intersect(o.Rectangle, Rectangle).IsEmpty)
                    {
                        jump = false;
                        break;
                    }
                }

                if (jump)
                {
                    Jump();
                }
            }
        }

        public override void TouchWithWall()
        {
            if (_rnd.Next(0, 2) == 0)
            {
                ControlDirectionX *= -1;
            }
            else
            {
                Jump();
            }
        }
    }
}