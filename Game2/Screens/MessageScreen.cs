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

        internal MessageScreen(ref Game2 game2) : base(ref game2)
        {
            Game2 = game2;
        }

        internal override void Draw(GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            if (Item != null)
            {
                Item.Color = Color;
                Item.Draw(ref spriteBatch, ref Game2.Font);
            }
        }

        internal Vector2 GetMsgSize(string msg, float scale)
        {
            return Utility.GetMsgSize(ref Game2.Font, msg, scale);
        }
    }
}
