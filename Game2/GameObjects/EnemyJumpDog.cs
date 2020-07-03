using System;

namespace Game2.GameObjects
{
    internal class EnemyJumpDog : EnemyDog
    {
        private readonly Random _rnd = new Random();

        internal EnemyJumpDog(Game2 game2, float x, float y) : base(game2, x, y)
        {
        }

        internal override void TouchWall()
        {
            if (_rnd.Next(0, 2) == 0)
            {
                base.TouchWall();
            }
            else
            {
                Jump();
            }
        }
    }
}