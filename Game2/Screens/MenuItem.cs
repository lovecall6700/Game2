using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Screens
{
    /// <summary>
    /// メニュー項目
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// 描画位置
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// 文言
        /// </summary>
        public string Menu;

        /// <summary>
        /// 文字倍率
        /// </summary>
        private readonly float Scale;

        /// <summary>
        /// 表示色
        /// </summary>
        public Color Color;

        /// <summary>
        /// 無効
        /// </summary>
        public bool Disable;

        public MenuItem(Vector2 position, string menu, float scale)
        {
            Position = position;
            Menu = menu;
            Scale = scale;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (Disable)
            {
                Color = Color.DarkSlateGray;
            }

            spriteBatch.DrawString(font, Menu, Position, Color, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }
    }
}
