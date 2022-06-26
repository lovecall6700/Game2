using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Managers
{
    /// <summary>
    /// 文字の画面表示
    /// </summary>
    internal class DigitalDisplay
    {
        internal Game2 Game2;
        internal GraphicsDevice Device;

        /// <summary>
        /// フォーマット
        /// </summary>
        internal string Format;

        /// <summary>
        /// 描画位置
        /// </summary>
        internal Vector2 Position = Vector2.Zero;

        /// <summary>
        /// フォント
        /// </summary>
        internal SpriteFont Font;

        /// <summary>
        /// 表示倍率
        /// </summary>
        internal float Scale = 1f;

        /// <summary>
        /// タイマー
        /// </summary>
        internal int Value;

        internal DigitalDisplay(ref Game2 game2, ref SpriteFont font, GraphicsDevice device)
        {
            Game2 = game2;
            Value = 0;
            Format = "{0:0000000000}";
            Font = font;
            Initialize(device);
        }

        internal virtual void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, string.Format(Format, Value), Game2.Camera2D.Position + Position, Color.White, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

        internal virtual void Update(ref GameTime gameTime)
        {

        }

        internal virtual void Initialize(GraphicsDevice device)
        {
            Device = device;
        }
    }
}
