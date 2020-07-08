﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Screens
{
    /// <summary>
    /// オプション画面
    /// </summary>
    internal class OptionsScreen : SelectScreen
    {
        private readonly MenuItem _item;

        internal OptionsScreen(Game2 game2, SpriteFont font) : base(game2, font)
        {
            _item = new MenuItem(new Vector2(80, 70), "Options", 1.5f)
            {
                Color = Color.White
            };

            Items.Add(new MenuItem(new Vector2(60, 120), "BGM Volume", 1.5f));
            Items.Add(new MenuItem(new Vector2(60, 145), "SE Volume", 1.5f));
            Items.Add(new MenuItem(new Vector2(60, 170), "End", 1.5f));
            Game2.MusicPlayer.PlaySong($"Songs/BGM9");
        }

        internal override void Draw(ref Vector2 offset, ref GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            _item.Draw(ref spriteBatch, ref Font);
            base.Draw(ref offset, ref gameTime, ref spriteBatch);
        }

        internal override void SelectMenu()
        {
            switch (Index)
            {
                case 0:

                    Game2.Scheduler.BGMVolume();
                    break;

                case 1:

                    Game2.Scheduler.SEVolume();
                    break;

                case 2:

                    Game2.Scheduler.Title();
                    break;
            }
        }
    }
}