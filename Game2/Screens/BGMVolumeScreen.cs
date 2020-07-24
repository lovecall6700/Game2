using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Screens
{
    /// <summary>
    /// BGM音量変更画面
    /// </summary>
    internal class BGMVolumeScreen : SelectScreen
    {
        private readonly MenuItem _item;

        internal BGMVolumeScreen(Game2 game2, SpriteFont font) : base(game2, font)
        {
            _item = new MenuItem(new Vector2(60, 70), "BGM Volume", 1.5f)
            {
                Color = Color.White
            };

            AddMenuItem(128, 110, "100%", 1f);
            AddMenuItem(128, 130, "80%", 1f);
            AddMenuItem(128, 150, "50%", 1f);
            AddMenuItem(128, 170, "25%", 1f);
            AddMenuItem(128, 190, "Mute", 1f);
            AddMenuItem(128, 210, "End", 1f);
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
                case 5:

                    Game2.Scheduler.Options();
                    break;

                default:

                    float v = 1f - 0.25f * Index;
                    Game2.MusicPlayer.SetSongVolume(v);
                    Game2.MusicPlayer.SaveSoundVolume();
                    break;
            }
        }
    }
}
