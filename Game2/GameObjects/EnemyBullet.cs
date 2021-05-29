using Microsoft.Xna.Framework;

namespace Game2.GameObjects
{
    internal class EnemyBullet : Enemy
    {
        /// <summary>
        /// /弾速
        /// </summary>
        private static readonly int bulletSpeed = 9;

        internal EnemyBullet(ref Game2 game2, float x, float y, float angle) : base(ref game2, x, y)
        {
            ObjectKind = GameObjectKinds.PlayerBullet;
            Vector2 upVector = new Vector2(0.0f, -1.0f);
            Matrix rotation = Matrix.CreateRotationZ(MathHelper.ToRadians(angle));
            Vector2 direction = Vector2.Transform(upVector, rotation);
            Velocity = direction * bulletSpeed;
            MaxSpeedX = 10;
            LifeTime = 800f;
            Img = Game2.Textures.GetTexture("EnemyBullet");
            SetSize(8, 8);
        }

        internal override bool MoveLeftOrRight(ref GameTime gameTime)
        {
            Position += Velocity;
            Rectangle.Location = Position.ToPoint();
            return false;
        }

        internal override bool JumpAndGravity(ref GameTime gameTime)
        {
            return false;
        }

        internal override void FinallyUpdate(ref GameTime gameTime)
        {
            foreach (GameObject o in Game2.PlaySc.NearMapObjs)
            {
                if (o.ObjectKind == GameObjectKinds.Cloud || o.ObjectKind == GameObjectKinds.Ladder || o.ObjectKind == GameObjectKinds.Disable)
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
