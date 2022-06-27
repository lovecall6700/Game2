using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Screens
{
    /// <summary>
    /// 画面の親
    /// </summary>
    internal class Screen
    {
        internal Game2 Game2;

        internal Screen(ref Game2 game2)
        {
            Game2 = game2;
            Game2.Camera2D.Focus(0, 0);
        }

        internal virtual void Draw(GameTime gameTime, ref SpriteBatch spriteBatch)
        {
        }

        internal virtual void Update(GameTime gameTime)
        {
        }
    }
}
