using Game2.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Screens
{
    /// <summary>
    /// ゲームオーバー画面
    /// </summary>
    internal class GameoverScreen : SelectScreen
    {
        private readonly MenuItem _item;

        /// <summary>
        /// セーブは一回だけ
        /// </summary>
        private bool _saveOnce = false;

        internal GameoverScreen(Game2 game2, SpriteFont font) : base(game2, font)
        {
            _item = new MenuItem(new Vector2(45, 70), "GAME OVER", 2f)
            {
                Color = Color.White
            };

            AddMenuItem(128, 140, "Retry", 1.5f);
            AddMenuItem(128, 170, "Save", 1.5f);
            AddMenuItem(128, 200, "End", 1.5f);
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

                    if (Game2.GameCtrl.Right && Game2.GameCtrl.Left)
                    {
                        Game2.Session.InfiniteItem = true;
                        Game2.Session.EnableTime = false;
                    }

                    Game2.Scheduler.SetSchedule(Schedule.Retry);
                    break;

                case 1:

                    if (!_saveOnce)
                    {
                        _saveOnce = true;
                        Items[1].Menu = "Saved";
                        Vector2 v = Utility.GetMsgSize(Font, "Saved", 1.5f) / 2; ;
                        Items[1].Position.X = 128 - v.X;
                        Items[1].Disable = true;
                        Index = 0;
                        Game2.Scheduler.SetSchedule(Schedule.SaveStage);
                    }

                    break;

                case 2:

                    Game2.Scheduler.SetSchedule(Schedule.Title);
                    break;
            }
        }
    }
}
