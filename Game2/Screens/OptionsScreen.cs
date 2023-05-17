using Game2.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Screens
{
    /// <summary>
    /// オプション画面
    /// </summary>
    public class OptionsScreen : SelectScreen
    {
        private readonly MenuItem _item;

        public OptionsScreen(Game2 game2) : base(game2)
        {
            _item = new MenuItem(new Vector2(80, 70), "Options", 1.5f)
            {
                Color = Color.White
            };

            AddMenuItem(128, 120, "BGM Volume", 1.5f);
            AddMenuItem(128, 145, "SE Volume", 1.5f);
            AddMenuItem(128, 170, "End", 1.5f);
            Game2.MusicPlayer.PlaySong($"Songs/BGM9");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            _item.Draw(spriteBatch, Game2.Font);
        }

        public override void SelectMenu()
        {
            switch (Index)
            {
                case 0:

                    Game2.Scheduler.SetSchedule(Schedules.BGMVolume);
                    break;

                case 1:

                    Game2.Scheduler.SetSchedule(Schedules.SEVolume);
                    break;

                case 2:

                    Game2.Scheduler.SetSchedule(Schedules.Title);
                    break;
            }
        }
    }
}
