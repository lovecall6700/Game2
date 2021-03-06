using Game2.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Screens
{
    /// <summary>
    /// SE音量変更画面
    /// </summary>
    internal class SEVolumeScreen : SelectScreen
    {
        private readonly MenuItem _item;

        internal SEVolumeScreen(ref Game2 game2, ref SpriteFont font) : base(ref game2, ref font)
        {
            _item = new MenuItem(new Vector2(60, 70), "SE Volume", 1.5f)
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

                    Game2.Scheduler.SetSchedule(Schedules.Options);
                    break;

                default:

                    float v = 1f - 0.25f * Index;
                    Game2.MusicPlayer.SetSEVolume(v);
                    Game2.MusicPlayer.SaveSoundVolume();
                    break;
            }
        }
    }
}
