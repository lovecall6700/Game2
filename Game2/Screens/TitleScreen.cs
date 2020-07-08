﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Screens
{
    /// <summary>
    /// タイトル画面
    /// </summary>
    internal class TitleScreen : SelectScreen
    {
        /// <summary>
        /// タイトルロゴ
        /// </summary>
        private readonly Texture2D _titleImg;

        internal TitleScreen(Game2 game2, SpriteFont font) : base(game2, font)
        {
            Items.Add(new MenuItem(new Vector2(70, 140), "Start", 1.2f));
            Items.Add(new MenuItem(new Vector2(70, 162), "Continue", 1.2f));
            Items.Add(new MenuItem(new Vector2(70, 184), "Options", 1.2f));
            Items.Add(new MenuItem(new Vector2(70, 208), "End", 1.2f));
            Game2.MusicPlayer.PlaySong($"Songs/BGM1");
            _titleImg = Game2.Textures.GetTexture("Images/Title");
        }

        internal override void Draw(ref Vector2 offset, ref GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            base.Draw(ref offset, ref gameTime, ref spriteBatch);
            spriteBatch.Draw(_titleImg, Vector2.Zero, Color.White);
        }

        internal override void SelectMenu()
        {
            switch (Index)
            {
                case 0:
                    Game2.Scheduler.InitialStart();
                    break;

                case 1:
                    Game2.Scheduler.ContinueStart();
                    break;

                case 2:
                    Game2.Scheduler.Options();
                    break;

                case 3:
                    Game2.Scheduler.Quit();
                    break;
            }
        }
    }
}