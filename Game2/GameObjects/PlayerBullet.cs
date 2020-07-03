using Game2.Utilities;
using Microsoft.Xna.Framework;

namespace Game2.GameObjects
{
    /// <summary>
    /// プレーヤー弾丸
    /// </summary>
    internal class PlayerBullet : PhysicsObject
    {
        /// <summary>
        /// 消えるまでの時間
        /// </summary>
        private readonly Timer _timer = new Timer();

        /// <summary>
        /// /弾速
        /// </summary>
        private static readonly int bulletSpeed = 12;

        internal PlayerBullet(Game2 game2, float x, float y, int direction) : base(game2, x, y)
        {
            ObjectKind = GameObjectKind.PlayerBullet;
            ControlDirectionX = direction;

            if (direction == -1)
            {
                Img = Game2.Textures.GetTexture("Images/PlayerBulletL");
            }
            else if (direction == 1)
            {
                Img = Game2.Textures.GetTexture("Images/PlayerBulletR");
            }

            Velocity = new Vector2(direction * bulletSpeed, 0);
            MaxSpeedX = 12;
            UseAirFriction = false;
            Gravity = 0.3f;
            _timer.Start(500f, true);
            SetSize(8, 8);
            Attack = 1;
        }

        internal override void Update(ref GameTime gameTime)
        {
            _timer.Update(ref gameTime);

            if (!_timer.Running)
            {
                ObjectStatus = PhysicsObjectStatus.Remove;
            }

            if (MoveLeftOrRight() | JumpAndGravity())
            {
                ObjectStatus = PhysicsObjectStatus.Remove;
            }
            else
            {
                foreach (PhysicsObject o in Game2.PlaySc.PhysicsObjs)
                {
                    if (o.ObjectKind != GameObjectKind.Enemy)
                    {
                        continue;
                    }

                    if (Rectangle.Intersect(o.Rectangle, Rectangle).IsEmpty)
                    {
                        continue;
                    }

                    ObjectStatus = PhysicsObjectStatus.Remove;

                    if (Game2.Inventory.HasSwordItem())
                    {
                        o.Damage(255);
                    }
                    else
                    {
                        o.Damage(Attack);
                    }
                }
            }
        }
    }
}
