using Microsoft.Xna.Framework;

namespace Game2.GameObjects
{
    public class EnemyNeedle : Enemy
    {
        public EnemyNeedle(Game2 game2, float x, float y) : base(game2, x, y)
        {
            Img = Game2.Textures.GetTexture("EnemyNeedle");
            UseAnimation = false;
        }

        public override bool MoveLeftOrRight(GameTime gameTime)
        {
            return false;
        }

        public override bool JumpAndGravity(GameTime gameTime)
        {
            return false;
        }
    }
}