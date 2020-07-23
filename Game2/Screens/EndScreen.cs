using Game2.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

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
        private readonly ImageList _img = new ImageList();

        /// <summary>
        /// 画像の位置
        /// </summary>
        private readonly List<Vector2> _position = new List<Vector2>();

        /// <summary>
        /// 画像枚数
        /// </summary>
        private static readonly int numOfImage = 5;

        internal EndScreen(Game2 game2, SpriteFont font) : base(game2, font)
        {
            for (int i = 0; i < numOfImage; i++)
            {
                _img.AddImage(Game2.Textures.GetTexture($"Images/End{i + 1}"));
                _position.Add(new Vector2(0, 256 + i * 128));
            }

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
                Vector2 v;

                for (int i = 0; i < numOfImage; i++)
                {
                    v = _position[i];
                    v.Y -= 1;
                    _position[i] = v;
                }

                if (_position[numOfImage - 1].Y == 0)
                {
                    Timer.Start(5000, true);
                }
            }
            if (_state == 2)
            {
                if (Game2.GameCtrl.IsClick(Managers.KeyName.Fire))
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

                for (int i = 0; i < numOfImage; i++)
                {
                    spriteBatch.Draw(_img.GetImage(i), _position[i], Color.White);
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
