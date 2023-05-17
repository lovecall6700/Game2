using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game2.GameObjects
{
    /// <summary>
    /// 物体
    /// </summary>
    public class GameObject
    {
        public Game2 Game2;

        /// <summary>
        /// 乗ってるオブジェクト
        /// </summary>
        public List<PhysicsObject> Connection = new List<PhysicsObject>();

        /// <summary>
        /// Texture2D
        /// </summary>
        public Rectangle? Img = null;

        /// <summary>
        /// 位置
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// 幅
        /// </summary>
        public int Width = 16;

        /// <summary>
        /// 高さ
        /// </summary>
        public int Height = 16;

        /// <summary>
        /// 接触判定用の矩形領域
        /// </summary>
        public Rectangle Rectangle = new Rectangle();

        /// <summary>
        /// 生成位置X
        /// </summary>
        public Vector2 Origin;

        /// <summary>
        /// ゲームオブジェクト種別
        /// </summary>
        public GameObjectKinds ObjectKind;

        /// <summary>
        /// 摩擦力
        /// </summary>
        private readonly float _friction = 2f;

        /// <summary>
        /// 大気の摩擦力
        /// </summary>
        private static readonly float airFriction = 1f;

        public GameObject(Game2 game2, float x, float y)
        {
            Game2 = game2;
            Position = new Vector2(x, y);
            Origin = new Vector2(x, y);
            Rectangle.Location = Position.ToPoint();
            SetSize(Width, Height);
        }

        /// <summary>
        /// オブジェクトの接触サイズを設定する
        /// </summary>
        /// <param name="w">幅</param>
        /// <param name="h">高さ</param>
        public void SetSize(int w, int h)
        {
            Width = w;
            Height = h;
            Rectangle.Width = w;
            Rectangle.Height = h;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Img == null || ObjectKind == GameObjectKinds.Disable)
            {
                return;
            }

            spriteBatch.Draw(Game2.Images, Position, Img, Color.White);
        }

        public virtual void Update()
        {
            if (Connection.Count > 0)
            {
                Connection.Clear();
            }
        }

        /// <summary>
        /// 摩擦力を返す
        /// </summary>
        /// <param name="velocity">速度</param>
        /// <param name="controlDirection">移動方向</param>
        /// <returns>摩擦力</returns>
        public virtual float GetFriction(float velocity, float controlDirection)
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
        /// 大気の摩擦力を返す
        /// </summary>
        /// <param name="velocity">速度</param>
        /// <returns>摩擦力</returns>
        public static float GetAirFriction(float velocity)
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

        /// <summary>
        /// 上に乗った物体との接続を持つ
        /// </summary>
        /// <param name="o">PhysicsObject</param>
        public void AddConnection(PhysicsObject o)
        {
            Connection.Add(o);
            o.GroundBlock = this;
        }

        /// <summary>
        /// 画面外に出た時の処理
        /// </summary>
        public virtual void Outside()
        {

        }

        /// <summary>
        /// リスタート時のリセット処理
        /// </summary>
        public virtual void Restart()
        {

        }
    }
}
