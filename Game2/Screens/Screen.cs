using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Screens
{
    /// <summary>
    /// 画面の親
    /// </summary>
    public class Screen
    {
        public Game2 Game2;

        private Color BackGroundColor = Color.Black;

        public Screen(Game2 game2)
        {
            Game2 = game2;
            Game2.Camera2D.Focus(0, 0);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual Color GetBackColor()
        {
            return BackGroundColor;
        }

        public virtual void FocusCamera2D()
        {
        }
    }
}
