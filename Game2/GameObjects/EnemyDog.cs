using Microsoft.Xna.Framework;

namespace Game2.GameObjects
{
    internal class EnemyDog : Enemy
    {
        internal EnemyDog(Game2 game2, float x, float y) : base(game2, x, y)
        {
            RImg.ClearAndAddImage(Game2.Textures.GetTexture("EnemyDogR1"));
            RImg.AddImage(Game2.Textures.GetTexture("EnemyDogR2"));
            LImg.ClearAndAddImage(Game2.Textures.GetTexture("EnemyDogL1"));
            LImg.AddImage(Game2.Textures.GetTexture("EnemyDogL2"));
            DeadImg = Game2.Textures.GetTexture("EnemyDogDead");
            Velocity = Vector2.Zero;
            ControlDirectionX = 0;
            MaxSpeedX = 4;
        }

        internal override void TouchWithWall()
        {
            ControlDirectionX *= -1;
        }
    }
}