using Microsoft.Xna.Framework;

namespace Game2.GameObjects
{
    internal class EnemyBullet : Enemy
    {
        /// <summary>
        /// /弾速
        /// </summary>
        private static readonly int bulletSpeed = 9;

        internal EnemyBullet(Game2 game2, float x, float y, float angle) : base(game2, x, y)
        {
            ObjectKind = GameObjectKind.PlayerBullet;
            Vector2 upVector = new Vector2(0.0f, -1.0f);
            Matrix rotation = Matrix.CreateRotationZ(MathHelper.ToRadians(angle));
            Vector2 direction = Vector2.Transform(upVector, rotation);
            Velocity = direction * bulletSpeed;
            MaxSpeedX = 10;
            LifeTime = 800f;
            Img = Game2.Textures.GetTexture("Images/EnemyBullet");
            SetSize(8, 8);
        }

        internal override void Update(ref GameTime gameTime)
        {
            UpdateLifeTime(ref gameTime);

            if (ObjectStatus != PhysicsObjectStatus.Normal)
            {
                return;
            }

            Position += Velocity;
            Rectangle.Location = Position.ToPoint();
            AttackPlayer();

            foreach (GameObject o in Game2.PlaySc.NearMapObjs)
            {
                if (o.ObjectKind == GameObjectKind.Cloud)
                {
                    continue;
                }
                else if (o.ObjectKind == GameObjectKind.Ladder)
                {
                    continue;
                }
                else if (Rectangle.Intersect(o.Rectangle, Rectangle).IsEmpty)
                {
                    continue;
                }

                ObjectStatus = PhysicsObjectStatus.Remove;
                break;
            }
        }
    }
}
