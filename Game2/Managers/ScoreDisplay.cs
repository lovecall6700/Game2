using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Managers
{
    /// <summary>
    /// 得点管理
    /// </summary>
    public class ScoreDisplay : DigitalDisplay
    {
        public ScoreDisplay(Game2 game2, GraphicsDevice device) : base(game2, device)
        {
            Initialize(device);
        }

        public override void Initialize(GraphicsDevice device)
        {
            base.Initialize(device);
            Position = new Vector2(155, 5);
        }

        /// <summary>
        /// 得点を加算する
        /// </summary>
        /// <param name="score">得点</param>
        public void AddScore(int score)
        {
            Value += score;
        }

        /// <summary>
        /// ハイスコアを描画する
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        public virtual void DrawHighScore(SpriteBatch spriteBatch)
        {
            Format = "HI{0:00000000}";
            spriteBatch.DrawString(Game2.Font, string.Format(Format, Game2.Session.HighScore), Game2.Camera2D.Position + Position, Color.White, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Format = "  {0:00000000}";
            base.Draw(spriteBatch);
        }

        public void TitleToInitialStart()
        {
            Value = 0;
        }

        public void GameoverRetryToStart()
        {
            Value = 0;
        }

        public void TitleToLoadStart()
        {
            Value = 0;
        }
    }
}
