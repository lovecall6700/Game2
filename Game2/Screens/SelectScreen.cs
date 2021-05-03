using Game2.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game2.Screens
{
    /// <summary>
    /// 選択肢画面
    /// </summary>
    internal class SelectScreen : Screen
    {
        /// <summary>
        /// 選択されているインデックス
        /// </summary>
        internal int Index = 0;

        /// <summary>
        /// 選択肢
        /// </summary>
        internal List<MenuItem> Items = new List<MenuItem>();

        /// <summary>
        /// 選択されている項目の色
        /// </summary>
        internal Color SelectedColor = Color.White;

        /// <summary>
        /// 選択されていない項目の色
        /// </summary>
        internal Color NotSelectedColor = Color.Gray;

        /// <summary>
        /// フォント
        /// </summary>
        internal SpriteFont Font;

        private bool _keyFlag = true;

        private readonly Timer _timer = new Timer();

        internal SelectScreen(Game2 game2, SpriteFont font) : base(game2)
        {
            Game2 = game2;
            Font = font;
        }

        internal void AddMenuItem(float x, float y, string menu, float scale)
        {
            Vector2 v = Utility.GetMsgSize(Font, menu, scale) / 2;
            v.Y = 0;
            Items.Add(new MenuItem(new Vector2(x, y) - v, menu, scale));
        }

        internal override void Draw(ref Vector2 offset, ref GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Index == i)
                {
                    Items[i].Color = SelectedColor;
                }
                else
                {
                    Items[i].Color = NotSelectedColor;
                }

                Items[i].Draw(ref spriteBatch, ref Font);
            }
        }

        internal override void Update(ref Vector2 offset, ref GameTime gameTime)
        {
            //一度離すのを確認してから入力を受け付ける
            if (_keyFlag)
            {
                if (Game2.GameCtrl.IsRelease(Managers.ButtonNames.Fire))
                {
                    _keyFlag = false;
                }

                return;
            }

            _timer.Update(ref gameTime);

            if (!_timer.Running && (Game2.GameCtrl.IsClick(Managers.ButtonNames.Up) || Game2.GameCtrl.IsRepeat(Managers.ButtonNames.Up)))
            {
                //上が押された
                Index = MathHelper.Clamp(Index - 1, 0, Items.Count - 1);

                if (Items[Index].Disable)
                {
                    Index = MathHelper.Clamp(Index - 1, 0, Items.Count - 1);
                }

                Game2.MusicPlayer.PlaySE("SoundEffects/MenuChange");
                PushUp();
                _timer.Start(180f, true);
            }
            else if (!_timer.Running && (Game2.GameCtrl.IsClick(Managers.ButtonNames.Down) || Game2.GameCtrl.IsRepeat(Managers.ButtonNames.Down)))
            {
                //下が押された
                Index = MathHelper.Clamp(Index + 1, 0, Items.Count - 1);

                //連続で無効は想定していない
                if (Items[Index].Disable)
                {
                    Index = MathHelper.Clamp(Index + 1, 0, Items.Count - 1);
                }

                Game2.MusicPlayer.PlaySE("SoundEffects/MenuChange");
                PushDown();
                _timer.Start(180f, true);
            }
            else if (Game2.GameCtrl.IsClick(Managers.ButtonNames.Fire))
            {
                //選択が押された
                SelectMenu();
                Game2.MusicPlayer.PlaySE("SoundEffects/MenuSelect");
                PushFire();
            }
        }

        internal virtual void PushUp()
        {
        }

        internal virtual void PushDown()
        {
        }

        internal virtual void PushFire()
        {
        }

        /// <summary>
        /// 選択肢が選択された場合の処理
        /// </summary>
        internal virtual void SelectMenu()
        {
        }
    }
}
