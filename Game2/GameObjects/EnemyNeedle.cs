using Microsoft.Xna.Framework;

namespace Game2.GameObjects
{
    internal class EnemyNeedle : Enemy
    {
        internal EnemyNeedle(Game2 game2, float x, float y) : base(game2, x, y)
        {
            Img = Game2.Textures.GetTexture("Images/EnemyNeedle");
        }

        internal override void Update(ref GameTime gameTime)
        {
            if (ObjectStatus == PhysicsObjectStatus.Normal)
            {
                AttackPlayer();
            }

            UpdateLifeTime(ref gameTime);
        }
    }
}