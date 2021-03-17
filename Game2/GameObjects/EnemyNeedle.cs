using Microsoft.Xna.Framework;

namespace Game2.GameObjects
{
    internal class EnemyNeedle : Enemy
    {
        internal EnemyNeedle(Game2 game2, float x, float y) : base(game2, x, y)
        {
            Img = Game2.Textures.GetTexture("EnemyNeedle");
            UseAnimation = false;
        }

        internal override bool MoveLeftOrRight(ref GameTime gameTime)
        {
            return false;
        }

        internal override bool JumpAndGravity(ref GameTime gameTime)
        {
            return false;
        }
    }
}