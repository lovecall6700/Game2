using Game2.Managers;
using Game2.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Screens
{
    /// <summary>
    /// ゲームオーバー画面
    /// </summary>
    public class GameoverScreen : SelectScreen
    {
        private readonly MenuItem _item;

        /// <summary>
        /// セーブは一回だけ
        /// </summary>
        private bool _saveOnce = false;

        public GameoverScreen(Game2 game2) : base(game2)
        {
            _item = new MenuItem(new Vector2(45, 70), "GAME OVER", 2f)
            {
                Color = Color.White
            };

            AddMenuItem(128, 140, "Retry", 1.5f);
            AddMenuItem(128, 170, "Save", 1.5f);
            AddMenuItem(128, 200, "End", 1.5f);
            Game2.MusicPlayer.PlaySong($"Songs/BGM9");
            WaitTimer.Start(45);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _item.Draw(spriteBatch, Game2.Font);

            if (WaitTimer.Running)
            {
                return;
            }

            base.Draw(gameTime, spriteBatch);
        }

        public override void SelectMenu()
        {
            switch (Index)
            {
                case 0:

                    if (Game2.GameCtrl.Right && Game2.GameCtrl.Left)
                    {
                        Game2.Session.InfiniteItem = true;
                        Game2.Session.EnableTime = false;
                    }

                    Game2.Scheduler.SetSchedule(Schedules.Retry);
                    break;

                case 1:

                    if (!_saveOnce)
                    {
                        _saveOnce = true;
                        Items[1].Menu = "Saved";
                        Vector2 v = Utility.GetMsgSize(Game2.Font, "Saved", 1.5f) / 2;
                        Items[1].Position.X = 128 - v.X;
                        Items[1].Disable = true;
                        Index = 0;
                        Game2.Scheduler.SetSchedule(Schedules.SaveStage);
                    }

                    break;

                case 2:

                    Game2.Scheduler.SetSchedule(Schedules.Title);
                    break;
            }
        }
    }
}
