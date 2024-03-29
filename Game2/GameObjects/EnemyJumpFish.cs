using Microsoft.Xna.Framework;

namespace Game2.GameObjects
{
    public class EnemyJumpFish : Enemy
    {
        public EnemyJumpFish(Game2 game2, float x, float y) : base(game2, x, y)
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

        public override bool MoveLeftOrRight(GameTime gameTime)
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

        public override bool JumpAndGravity(GameTime gameTime)
        {
            return false;
        }

        public override void Jump()
        {
            Velocity.Y = -JumpAcceleration;
        }
    }
}