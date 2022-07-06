using Microsoft.Xna.Framework;

namespace Game2.GameObjects
{
    public class MovingFloor : Block
    {
        private bool _dir = false;
        private readonly float _speed = 2f;
        public readonly bool Vertical = false;
        private readonly float _min = -50f;
        private readonly float _max = 50f;

        public MovingFloor(Game2 game2, float x, float y, string type, float min, float max) : base(game2, x, y)
        {
            Vertical = type == "Vertical";
            _dir = min < max;
            _min = MathHelper.Min(min, max);
            _max = MathHelper.Max(min, max);
            ObjectKind = GameObjectKinds.MovingFloor;
            Img = Game2.Textures.GetTexture("MovingFloor");
            SetSize(48, 10);
        }

        public void ResetPosition()
        {
            Position = Origin;
        }

        public override void Update(GameTime gameTime)
        {
            float s = (_dir ? -1f : 1f) * _speed;
            float d;

            if (Vertical)
            {
                d = Position.Y;
                Position.Y += s;

                if (Position.Y < Origin.Y + _min || Position.Y > Origin.Y + _max)
                {
                    Position.Y = MathHelper.Clamp(Position.Y, Origin.Y + _min, Origin.Y + _max);
                    _dir = !_dir;
                }

                d = Position.Y - d;
            }
            else
            {
                d = Position.X;
                Position.X += s;

                if (Position.X < Origin.X + _min || Position.X > Origin.X + _max)
                {
                    Position.X = MathHelper.Clamp(Position.X, Origin.X + _min, Origin.X + _max);
                    _dir = !_dir;
                }

                d = Position.X - d;
            }

            Rectangle.Location = Position.ToPoint();

            foreach (PhysicsObject o in Game2.PlaySc.PhysicsObjs)
            {
                if (o.ObjectStatus != PhysicsObjectStatus.Normal)
                {
                    continue;
                }

                if (Vertical)
                {
                    //上下移動
                    if (Connection.Contains(o))
                    {
                        //上に乗っている
                        o.Position.Y += d;
                        o.Rectangle.Location = o.Position.ToPoint();
                        Collision(o, true, d);
                    }
                    else if (!Rectangle.Intersect(o.Rectangle, Rectangle).IsEmpty)
                    {
                        //下側にいた
                        o.Position.Y += d;
                        o.Velocity.Y = d;
                        o.Rectangle.Location = o.Position.ToPoint();
                        Collision(o, true, d);
                    }
                }
                else
                {
                    //左右移動
                    if (Connection.Contains(o))
                    {
                        //上に乗っている
                        o.Position.X += d;
                        o.Rectangle.Location = o.Position.ToPoint();
                        Collision(o, false, d);
                    }
                    else if (!Rectangle.Intersect(o.Rectangle, Rectangle).IsEmpty)
                    {
                        //左右にいた
                        o.Position.X += d;
                        o.Rectangle.Location = o.Position.ToPoint();
                        Collision(o, true, d);
                    }
                }
            }

            base.Update(gameTime);
        }

        private void Collision(PhysicsObject p, bool crushable, float delta)
        {
            foreach (GameObject o in Game2.PlaySc.NearMapObjs)
            {
                if (o == this)
                {
                    continue;
                }
                else if (o.ObjectKind == GameObjectKinds.Cloud || o.ObjectKind == GameObjectKinds.Ladder || o.ObjectKind == GameObjectKinds.Disable)
                {
                    continue;
                }
                else if (Rectangle.Intersect(o.Rectangle, p.Rectangle).IsEmpty)
                {
                    continue;
                }

                if (crushable)
                {
                    //圧死する場合
                    p.Damage(255);
                    break;
                }

                //圧死しない場合
                //何かとぶつかったら、めり込まずに手前で止まる
                if (delta < 0)
                {
                    p.Rectangle.X = o.Rectangle.Right;
                    p.Position.X = p.Rectangle.X;
                }
                else if (delta > 0)
                {
                    p.Rectangle.X = o.Rectangle.Left - o.Width;
                    p.Position.X = p.Rectangle.X;
                }
            }
        }
    }
}