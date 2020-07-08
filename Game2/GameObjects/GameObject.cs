using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game2.GameObjects
{
    /// <summary>
    /// 物体
    /// </summary>
    internal partial class GameObject
    {
        internal Game2 Game2;

        /// <summary>
        /// 乗ってるオブジェクト
        /// </summary>
        internal List<PhysicsObject> Connection = new List<PhysicsObject>();

        /// <summary>
        /// Texture2D
        /// </summary>
        internal Texture2D Img = null;

        /// <summary>
        /// 位置
        /// </summary>
        internal Vector2 Position;

        /// <summary>
        /// 幅
        /// </summary>
        internal int Width = 16;

        /// <summary>
        /// 高さ
        /// </summary>
        internal int Height = 16;

        /// <summary>
        /// 接触判定用の矩形領域
        /// </summary>
        internal Rectangle Rectangle = new Rectangle();

        /// <summary>
        /// 生成位置X
        /// </summary>
        internal Vector2 Origin;

        /// <summary>
        /// ゲームオブジェクト種別
        /// </summary>
        internal GameObjectKind ObjectKind;

        /// <summary>
        /// 摩擦
        /// </summary>
        private readonly float _friction = 2f;

        /// <summary>
        /// 空中摩擦
        /// </summary>
        private static readonly float airFriction = 1f;

        internal GameObject(Game2 game2, float x, float y)
        {
            Game2 = game2;
            Position = new Vector2(x, y);
            Origin = new Vector2(x, y);
            Rectangle.Location = Position.ToPoint();
            SetSize(Width, Height);
        }

        internal void SetSize(int w, int h)
        {
            Width = w;
            Height = h;
            Rectangle.Width = w;
            Rectangle.Height = h;
        }

        internal virtual void Draw(ref Vector2 offset, ref GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            if (Img == null)
            {
                return;
            }

            spriteBatch.Draw(Img, Position - offset, Color.White);
        }

        internal virtual void Update(ref GameTime gameTime)
        {
            Connection.Clear();
        }

        /// <summary>
        /// 摩擦力を返す
        /// </summary>
        /// <param name="velocity">速度</param>
        /// <param name="controlDirection">移動方向</param>
        /// <returns>摩擦力</returns>
        internal virtual float GetFriction(float velocity, float controlDirection)
        {
            //通常の場合は制御は無関係に摩擦力は進行方向と逆に働く
            if (velocity < 0)
            {
                return _friction;
            }
            else if (velocity > 0)
            {
                return -_friction;
            }

            return 0;
        }

        /// <summary>
        /// 空中の摩擦力を返す
        /// </summary>
        /// <param name="velocity">速度</param>
        /// <returns>摩擦力</returns>
        internal static float GetAirFriction(float velocity)
        {
            //通常の場合は制御は無関係に摩擦力は進行方向と逆に働く
            if (velocity < 0)
            {
                return airFriction;
            }
            else if (velocity > 0)
            {
                return -airFriction;
            }

            return 0;
        }

        internal void AddConnection(PhysicsObject o)
        {
            Connection.Add(o);
            o.GroundBlock = this;
        }
    }
}
