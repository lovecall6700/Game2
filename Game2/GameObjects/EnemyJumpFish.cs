using Microsoft.Xna.Framework;

namespace Game2.GameObjects
{
    internal class EnemyJumpFish : Enemy
    {
        internal EnemyJumpFish(Game2 game2, float x, float y) : base(game2, x, y)
        {
            RImg.ClearAndAddImage(Game2.Textures.GetTexture("EnemyJumpFishR1"));
            RImg.AddImage(Game2.Textures.GetTexture("EnemyJumpFishR2"));
            LImg.ClearAndAddImage(Game2.Textures.GetTexture("EnemyJumpFishL1"));
            LImg.AddImage(Game2.Textures.GetTexture("EnemyJumpFishL2"));
            DeadImg = Game2.Textures.GetTexture("EnemyJumpFishDead");
            SetSize(16, 16);
            Velocity = Vector2.Zero;
            MaxSpeedX = 5;
            JumpAcceleration = 20;
            MaxJumpSpeed = 20;
            UseOutOfMapY = false;
            AnimationAlways = true;
        }

        internal override bool MoveLeftOrRight(ref GameTime gameTime)
        {
            Velocity.X = ControlDirectionX * AccelerationX;
            Velocity.Y += Gravity;

            if (Velocity.Y > 0)
            {
                UseOutOfMapY = true;
            }

            Position += Velocity;
            Rectangle.Location = Position.ToPoint();
            return false;
        }

        internal override bool JumpAndGravity(ref GameTime gameTime)
        {
            return false;
        }

        internal override void Jump()
        {
            Velocity.Y = -JumpAcceleration;
        }
    }
}