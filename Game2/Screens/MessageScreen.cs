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

        internal MessageScreen(Game2 game2) : base(game2)
        {
            Game2 = game2;
        }

        internal override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Item != null)
            {
                Item.Color = Color;
                Item.Draw(spriteBatch, Game2.Font);
            }
        }

        internal Vector2 GetMsgSize(string msg, float scale)
        {
            return Utility.GetMsgSize(Game2.Font, msg, scale);
        }
    }
}
