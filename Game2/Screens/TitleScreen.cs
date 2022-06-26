using Game2.Managers;
using Game2.Utilities;
using Microsoft.Xna.Framework;
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
        private readonly Rectangle? _titleImg;

        /// <summary>
        /// ストーリ画面に遷移するまでの時間
        /// </summary>
        private readonly Timer _storyTimer = new Timer();

        internal TitleScreen(ref Game2 game2, ref SpriteFont font) : base(ref game2, ref font)
        {
            AddMenuItem(128, 140, "Start", 1.2f);
            AddMenuItem(128, 162, "Continue", 1.2f);
            AddMenuItem(128, 184, "Options", 1.2f);
            AddMenuItem(128, 208, "End", 1.2f);
            Game2.MusicPlayer.PlaySong($"Songs/BGM1");
            _titleImg = Game2.Textures.GetTexture("Title");
            _storyTimer.Start(8000f, true);
        }

        internal override void Draw(ref GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            base.Draw(ref gameTime, ref spriteBatch);
            spriteBatch.Draw(Game2.Images, Vector2.Zero, _titleImg, Color.White);
        }

        internal override void Update(ref GameTime gameTime)
        {
            if (!_storyTimer.Update(ref gameTime))
            {
                Game2.Scheduler.SetSchedule(Schedules.Story);
                return;
            }

            base.Update(ref gameTime);
        }

        internal override void PushUp()
        {
            _storyTimer.Start(8000f, true);
        }

        internal override void PushDown()
        {
            _storyTimer.Start(8000f, true);
        }

        internal override void PushFire()
        {
            _storyTimer.Start(8000f, true);
        }

        internal override void SelectMenu()
        {
            switch (Index)
            {
                case 0:
                    Game2.Scheduler.SetSchedule(Schedules.InitialStart);
                    break;

                case 1:
                    Game2.Scheduler.SetSchedule(Schedules.ContinueStart);
                    break;

                case 2:
                    Game2.Scheduler.SetSchedule(Schedules.Options);
                    break;

                case 3:
                    Game2.Scheduler.SetSchedule(Schedules.Quit);
                    break;
            }
        }
    }
}
