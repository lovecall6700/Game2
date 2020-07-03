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

        internal Screen(Game2 game2)
        {
            Game2 = game2;
        }

        internal virtual void Draw(ref Vector2 offset, ref GameTime gameTime, ref SpriteBatch spriteBatch)
        {
        }

        internal virtual void Update(ref Vector2 offset, ref GameTime gametime)
        {
        }
    }
}
