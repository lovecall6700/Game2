using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Screens
{
    /// <summary>
    /// メニュー項目
    /// </summary>
    internal class MenuItem
    {
        /// <summary>
        /// 描画位置
        /// </summary>
        internal Vector2 Position;

        /// <summary>
        /// 文言
        /// </summary>
        internal string Menu;

        /// <summary>
        /// 文字倍率
        /// </summary>
        private readonly float Scale;

        /// <summary>
        /// 表示色
        /// </summary>
        internal Color Color;

        /// <summary>
        /// 無効
        /// </summary>
        internal bool Disable;

        internal MenuItem(Vector2 position, string menu, float scale)
        {
            Position = position;
            Menu = menu;
            Scale = scale;
        }

        internal void Draw(ref SpriteBatch spriteBatch, ref SpriteFont font)
        {
            if (Disable)
            {
                Color = Color.DarkSlateGray;
            }

            spriteBatch.DrawString(font, Menu, Position, Color, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }
    }
}
