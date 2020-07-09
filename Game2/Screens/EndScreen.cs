using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Screens
{
    /// <summary>
    /// エンディング画面
    /// </summary>
    internal class EndScreen : TimerScreen
    {
        /// <summary>
        /// 画面の状態
        /// </summary>
        private int _state = 0;

        /// <summary>
        /// エンディング用画像
        /// </summary>
        private readonly Texture2D[] _img = new Texture2D[5];

        /// <summary>
        /// 画像の位置
        /// </summary>
        private readonly Vector2[] _position = new Vector2[5];

        internal EndScreen(Game2 game2, SpriteFont font) : base(game2, font)
        {
            _img[0] = Game2.Textures.GetTexture("Images/End1");
            _position[0].X = 0;
            _position[0].Y = 256;
            _img[1] = Game2.Textures.GetTexture("Images/End2");
            _position[1].X = 0;
            _position[1].Y = 384;
            _img[2] = Game2.Textures.GetTexture("Images/End3");
            _position[2].X = 0;
            _position[2].Y = 512;
            _img[3] = Game2.Textures.GetTexture("Images/End4");
            _position[3].X = 0;
            _position[3].Y = 640;
            _img[4] = Game2.Textures.GetTexture("Images/End5");
            _position[4].X = 0;
            _position[4].Y = 768;
            Item = new MenuItem(new Vector2(50, 100), "Congratulations!!", 1f);
            Game2.MusicPlayer.PlaySong($"Songs/BGM7");
            Timer.Start(2000, true);
        }

        internal override void Update(ref Vector2 offset, ref GameTime gameTime)
        {
            base.Update(ref offset, ref gameTime);

            if (_state == 0)
            {
            }
            else if (_state == 1)
            {
                Item.Position.Y -= 1;

                for (int i = 0; i < _position.Length; i++)
                {
                    _position[i].Y -= 1;
                }

                if (_position[4].Y == 0)
                {
                    Timer.Start(5000, true);
                }
            }
            if (_state == 2)
            {
                if (Game2.GameCtrl.Fire)
                {
                    Timer.Running = false;
                }
            }
        }

        internal override void Draw(ref Vector2 offset, ref GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            if (_state == 0)
            {
                base.Draw(ref offset, ref gameTime, ref spriteBatch);
            }
            else if (_state == 1)
            {
                base.Draw(ref offset, ref gameTime, ref spriteBatch);

                for (int i = 0; i < _position.Length; i++)
                {
                    spriteBatch.Draw(_img[i], _position[i], Color.White);
                }
            }
            if (_state == 2)
            {
                base.Draw(ref offset, ref gameTime, ref spriteBatch);
            }
        }

        internal override void Timeup()
        {
            if (_state == 0)
            {
                _state = 1;
                Timer.Start(30000, true);
            }
            else if (_state == 1)
            {
                _state = 2;
                Item = new MenuItem(new Vector2(75, 100), "Fin.", 3f);
                Timer.Start(30000, true);
            }
            else if (_state == 2)
            {
                Game2.Scheduler.Title();
            }
        }
    }
}
