using Microsoft.Xna.Framework;

namespace Game2.GameObjects
{
    internal class EnemyDog : Enemy
    {
        internal EnemyDog(Game2 game2, float x, float y) : base(game2, x, y)
        {
            RImg[0] = Game2.Textures.GetTexture("Images/EnemyDogR1");
            RImg[1] = Game2.Textures.GetTexture("Images/EnemyDogR2");
            LImg[0] = Game2.Textures.GetTexture("Images/EnemyDogL1");
            LImg[1] = Game2.Textures.GetTexture("Images/EnemyDogL2");
            DeadImg = Game2.Textures.GetTexture("Images/EnemyDogDead");
            Velocity = Vector2.Zero;
            ControlDirectionX = 0;
            MaxSpeedX = 4;
        }

        internal override void Update(ref GameTime gameTime)
        {
            UpdateLifeTime(ref gameTime);

            if (ObjectStatus != PhysicsObjectStatus.Normal)
            {
                return;
            }

            if (MoveLeftOrRight())
            {
                TouchWall();
            }

            JumpAndGravity();
            AttackPlayer();
            UpdateAnimationIndex();
        }

        internal virtual void TouchWall()
        {
            ControlDirectionX *= -1;
        }
    }
}