using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Managers
{
    /// <summary>
    /// 文字の画面表示
    /// </summary>
    public class DigitalDisplay
    {
        public Game2 Game2;
        public GraphicsDevice Device;

        /// <summary>
        /// フォーマット
        /// </summary>
        public string Format;

        /// <summary>
        /// 描画位置
        /// </summary>
        public Vector2 Position = Vector2.Zero;

        /// <summary>
        /// 表示倍率
        /// </summary>
        public float Scale = 1f;

        /// <summary>
        /// タイマー
        /// </summary>
        public int Value;

        public DigitalDisplay(Game2 game2, GraphicsDevice device)
        {
            Game2 = game2;
            Value = 0;
            Format = "{0:0000000000}";
            Initialize(device);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game2.Font, string.Format(Format, Value), Game2.Camera2D.Position + Position, Color.White, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Initialize(GraphicsDevice device)
        {
            Device = device;
        }
    }
}
