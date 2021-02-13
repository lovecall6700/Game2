using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Managers
{
    /// <summary>
    /// 得点管理
    /// </summary>
    internal class ScoreDisplay : DigitalDisplay
    {
        internal ScoreDisplay(Game2 game2, SpriteFont font, GraphicsDevice device) : base(game2, font, device)
        {
            Initialize(device);
        }

        internal override void Initialize(GraphicsDevice device)
        {
            base.Initialize(device);
            Position = new Vector2(155, 5);
        }

        /// <summary>
        /// 得点を加算する
        /// </summary>
        /// <param name="score">得点</param>
        internal void AddScore(int score)
        {
            Value += score;
        }

        /// <summary>
        /// ハイスコアを描画する
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        internal virtual void DrawHighScore(ref SpriteBatch spriteBatch)
        {
            Format = "HI{0:00000000}";
            spriteBatch.DrawString(Font, string.Format(Format, Game2.Session.HighScore), Position, Color.White, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

        internal override void Draw(ref SpriteBatch spriteBatch)
        {
            Format = "  {0:00000000}";
            base.Draw(ref spriteBatch);
        }

        internal void TitleToInitialStart()
        {
            Value = 0;
        }

        internal void GameoverRetryToStart()
        {
            Value = 0;
        }

        internal void TitleToLoadStart()
        {
            Value = 0;
        }
    }
}
