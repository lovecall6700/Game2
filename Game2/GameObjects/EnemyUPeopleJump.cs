using Microsoft.Xna.Framework;
using System;

namespace Game2.GameObjects
{
    internal class EnemyUPeopleJump : EnemyUPeopleBasic
    {
        private readonly Random _rnd = new Random();
        private Rectangle _jumpRect = new Rectangle(0, 0, 8, 16);

        internal EnemyUPeopleJump(Game2 game2, float x, float y) : base(game2, x, y)
        {
        }

        internal override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);

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
                    if (!Rectangle.Intersect(o.Rectangle, Rectangle).IsEmpty)
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

        internal override void TouchWall()
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