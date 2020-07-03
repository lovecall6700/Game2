using Microsoft.Xna.Framework;

namespace Game2.GameObjects
{
    internal class EnemyJumpFish : Enemy
    {
        internal EnemyJumpFish(Game2 game2, float x, float y) : base(game2, x, y)
        {
            RImg[0] = Game2.Textures.GetTexture("Images/EnemyJumpFishR1");
            RImg[1] = Game2.Textures.GetTexture("Images/EnemyJumpFishR2");
            LImg[0] = Game2.Textures.GetTexture("Images/EnemyJumpFishL1");
            LImg[1] = Game2.Textures.GetTexture("Images/EnemyJumpFishL2");
            DeadImg = Game2.Textures.GetTexture("Images/EnemyJumpFishDead");
            SetSize(16, 16);
            Velocity = Vector2.Zero;
            MaxSpeedX = 5;
            JumpAcceleration = 20;
            MaxJumpSpeed = 20;
            UseOutOfMapY = false;
            AnimationAlways = true;
        }

        internal override void Update(ref GameTime gameTime)
        {
            UpdateLifeTime(ref gameTime);

            if (ObjectStatus != PhysicsObjectStatus.Normal)
            {
                return;
            }

            Velocity.X = ControlDirectionX * AccelerationX;
            Velocity.Y += Gravity;

            if (Velocity.Y > 0)
            {
                UseOutOfMapY = true;
            }

            Position += Velocity;
            Rectangle.Location = Position.ToPoint();
            AttackPlayer();
            UpdateAnimationIndex();
        }

        internal override void Jump()
        {
            Velocity.Y = -JumpAcceleration;
        }
    }
}