using Game2.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Screens
{
    /// <summary>
    /// 1行文字表示の画面
    /// </summary>
    public class MessageScreen : Screen
    {
        /// <summary>
        /// メッセージ
        /// </summary>
        public MenuItem Item;

        /// <summary>
        /// 描画色
        /// </summary>
        public Color Color = Color.White;

        public MessageScreen(Game2 game2) : base(game2)
        {
            Game2 = game2;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Item != null)
            {
                Item.Color = Color;
                Item.Draw(spriteBatch, Game2.Font);
            }
        }

        public Vector2 GetMsgSize(string msg, float scale)
        {
            return Utility.GetMsgSize(Game2.Font, msg, scale);
        }
    }
}
