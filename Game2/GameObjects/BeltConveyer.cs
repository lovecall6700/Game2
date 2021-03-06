using Game2.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.GameObjects
{
    internal class BeltConveyer : Block
    {
        private readonly bool _left = false;
        private readonly float _speed = -4f;
        private readonly Rectangle? _beltImg;
        private readonly Timer _timer = new Timer();
        private readonly float _time = 100f;
        private bool _vibro = false;
        private Vector2 _vibroA = new Vector2(1f, 0);
        private Vector2 _vibroB = new Vector2(-1f, 0);

        internal BeltConveyer(ref Game2 game2, float x, float y, string dummy, string dir) : base(ref game2, x, y)
        {
            ObjectKind = GameObjectKinds.Block;
            Img = Game2.Textures.GetTexture("" + dummy);
            _beltImg = Game2.Textures.GetTexture("BeltConveyer");
            SetSize(16, 16);

            if (dir == "Left")
            {
                _left = false;
            }
            else
            {
                _left = true;
            }
        }

        internal override void Draw(ref Vector2 offset, ref GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            base.Draw(ref offset, ref gameTime, ref spriteBatch);
            spriteBatch.Draw(Game2.Images, Position - offset + (_vibro ? _vibroA : _vibroB), _beltImg, Color.White);
        }

        internal override void Update(ref GameTime gameTime)
        {
            if (!_timer.Update(ref gameTime))
            {
                _vibro = !_vibro;
                _timer.Start(_time, true);
            }

            float s = (_left ? -1f : 1f) * _speed;

            foreach (PhysicsObject o in Game2.PlaySc.PhysicsObjs)
            {
                if (o.ObjectStatus != PhysicsObjectStatus.Normal)
                {
                    continue;
                }

                //左右移動
                if (Connection.Contains(o))
                {
                    //上に乗っている
                    o.Position.X += s;
                    o.Rectangle.Location = o.Position.ToPoint();
                    Collision(o, s);
                }
            }

            base.Update(ref gameTime);
        }

        private void Collision(PhysicsObject p, float delta)
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