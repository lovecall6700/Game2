using Game2.Utilities;
using Microsoft.Xna.Framework;

namespace Game2.GameObjects
{
    /// <summary>
    /// プレーヤー弾丸
    /// </summary>
    public class PlayerBullet : PhysicsObject
    {
        /// <summary>
        /// 消えるまでの時間
        /// </summary>
        private readonly Timer _removeTimer = new Timer();

        /// <summary>
        /// /弾速
        /// </summary>
        private static readonly int bulletSpeed = 12;

        public PlayerBullet(Game2 game2, float x, float y, int direction) : base(game2, x, y)
        {
            ObjectKind = GameObjectKinds.PlayerBullet;
            ControlDirectionX = direction;

            if (direction == -1)
            {
                Img = Game2.Textures.GetTexture("PlayerBulletL");
            }
            else if (direction == 1)
            {
                Img = Game2.Textures.GetTexture("PlayerBulletR");
            }

            Velocity = new Vector2(direction * bulletSpeed, 0);
            MaxSpeedX = 12;
            UseAirFriction = false;
            Gravity = 0.3f;
            _removeTimer.Start(15);
            SetSize(8, 8);
            Attack = 1;
        }

        public override void Update()
        {
            if (!_removeTimer.Update())
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
                    if (o.ObjectKind != GameObjectKinds.Enemy)
                    {
                        continue;
                    }

                    if (Rectangle.Intersect(o.Rectangle, Rectangle).IsEmpty)
                    {
                        continue;
                    }

                    ObjectStatus = PhysicsObjectStatus.Remove;

                    if (Game2.Session.Inventory.HasSwordItem())
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
