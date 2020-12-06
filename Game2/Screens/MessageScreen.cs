using Game2.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Screens
{
    /// <summary>
    /// 1行文字表示の画面
    /// </summary>
    internal class MessageScreen : Screen
    {
        /// <summary>
        /// メッセージ
        /// </summary>
        internal MenuItem Item;

        /// <summary>
        /// 描画色
        /// </summary>
        internal Color Color = Color.White;

        /// <summary>
        /// 描画フォント
        /// </summary>
        internal SpriteFont Font;

        internal MessageScreen(Game2 game2, SpriteFont font) : base(game2)
        {
            Game2 = game2;
            Font = font;
        }

        internal override void Draw(ref Vector2 offset, ref GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            if (Item != null)
            {
                Item.Color = Color;
                Item.Draw(ref spriteBatch, ref Font);
            }
        }

        internal Vector2 GetMsgSize(string msg, float scale)
        {
            return Utility.GetMsgSize(Font, msg, scale);
        }
    }
}
