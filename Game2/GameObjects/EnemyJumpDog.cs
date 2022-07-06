using System;

namespace Game2.GameObjects
{
    public class EnemyJumpDog : EnemyDog
    {
        private readonly Random _rnd = new Random();

        public EnemyJumpDog(Game2 game2, float x, float y) : base(game2, x, y)
        {
        }

        public override void TouchWithWall()
        {
            if (_rnd.Next(0, 2) == 0)
            {
                base.TouchWithWall();
            }
            else
            {
                Jump();
            }
        }
    }
}