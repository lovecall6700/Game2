using Game2.Managers;
using Game2.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Screens
{
    /// <summary>
    /// BGM音量変更画面
    /// </summary>
    public class BGMVolumeScreen : SelectScreen
    {
        private readonly MenuItem _item;

        public BGMVolumeScreen(Game2 game2) : base(game2)
        {
            _item = new MenuItem(new Vector2(60, 70), "BGM Volume", 1.5f)
            {
                Color = Color.White
            };

            AddMenuItem(128, 110, "100%", 1f);
            AddMenuItem(128, 130, "75%", 1f);
            AddMenuItem(128, 150, "50%", 1f);
            AddMenuItem(128, 170, "25%", 1f);
            AddMenuItem(128, 190, "Mute", 1f);
            AddMenuItem(128, 210, "End", 1f);
            float volume = Game2.MusicPlayer.GetSongVolume();
            SelectVolumeItem(volume);

            Index = Utility.AlmostEqual(volume, 1.0f) ? 1 : 0;

            Game2.MusicPlayer.PlaySong($"Songs/BGM9");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _item.Draw(spriteBatch, Game2.Font);
            base.Draw(gameTime, spriteBatch);
        }

        public override void SelectMenu()
        {
            switch (Index)
            {
                case 5:

                    Game2.Scheduler.SetSchedule(Schedules.Options);
                    break;

                default:

                    float v = 1f - (0.25f * Index);
                    Game2.MusicPlayer.SetSongVolume(v);
                    Game2.MusicPlayer.SaveSoundVolume();
                    SelectVolumeItem(v);
                    break;
            }
        }

        /// <summary>
        /// ボリュームの選択を行う
        /// </summary>
        /// <param name="volume">ボリューム</param>
        public void SelectVolumeItem(float volume)
        {
            foreach (MenuItem item in Items)
            {
                item.Disable = false;
            }

            if (Utility.AlmostEqual(volume, 1.0f))
            {
                Items[0].Disable = true;
                Index = 1;
            }
            else if (Utility.AlmostEqual(volume, 0.75f))
            {
                Items[1].Disable = true;
                Index = 2;
            }
            else if (Utility.AlmostEqual(volume, 0.5f))
            {
                Items[2].Disable = true;
                Index = 3;
            }
            else if (Utility.AlmostEqual(volume, 0.25f))
            {
                Items[3].Disable = true;
                Index = 4;
            }
            else if (Utility.AlmostEqual(volume, 0.0f))
            {
                Items[4].Disable = true;
                Index = 0;
            }
        }
    }
}
