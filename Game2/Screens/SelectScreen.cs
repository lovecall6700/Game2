using Game2.Inputs;
using Game2.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game2.Screens
{
    /// <summary>
    /// 選択肢画面
    /// </summary>
    public class SelectScreen : Screen
    {
        /// <summary>
        /// 選択されているインデックス
        /// </summary>
        public int Index = 0;

        /// <summary>
        /// 選択肢
        /// </summary>
        public List<MenuItem> Items = new List<MenuItem>();

        /// <summary>
        /// 選択されている項目の色
        /// </summary>
        public Color SelectedColor = Color.White;

        /// <summary>
        /// 選択されていない項目の色
        /// </summary>
        public Color NotSelectedColor = Color.Gray;

        /// <summary>
        /// 画面が出てからしばらくは操作できない
        /// </summary>
        public readonly Timer WaitTimer = new Timer();

        private bool _keyFlag = true;

        public SelectScreen(Game2 game2) : base(game2)
        {
            Game2 = game2;
            WaitTimer.Start(100f, true);
        }

        public void AddMenuItem(float x, float y, string menu, float scale)
        {
            Vector2 v = Utility.GetMsgSize(Game2.Font, menu, scale) / 2;
            v.Y = 0;
            Items.Add(new MenuItem(new Vector2(x, y) - v, menu, scale));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
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

                Items[i].Draw(spriteBatch, Game2.Font);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (WaitTimer.Update(gameTime))
            {
                return;
            }

            //一度離すのを確認してから入力を受け付ける
            if (_keyFlag)
            {
                if (Game2.GameCtrl.IsRelease(ButtonNames.Fire) &&
                    Game2.GameCtrl.IsRelease(ButtonNames.Up) &&
                    Game2.GameCtrl.IsRelease(ButtonNames.Down))
                {
                    _keyFlag = false;
                }

                return;
            }

            if (Game2.GameCtrl.IsClick(ButtonNames.Up))
            {
                //上が押された
                Index = MathHelper.Clamp(Index - 1, 0, Items.Count - 1);

                if (Items[Index].Disable)
                {
                    if (Index == 0)
                    {
                        Index = 1;
                    }
                    else
                    {
                        Index = MathHelper.Clamp(Index - 1, 0, Items.Count - 1);
                    }
                }

                Game2.MusicPlayer.PlaySE("SoundEffects/MenuChange");
                PushUp();
            }
            else if (Game2.GameCtrl.IsClick(ButtonNames.Down))
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
            }
            else if (Game2.GameCtrl.IsClick(ButtonNames.Fire))
            {
                //選択が押された
                SelectMenu();
                Game2.MusicPlayer.PlaySE("SoundEffects/MenuSelect");
                PushFire();
            }
        }

        public virtual void PushUp()
        {
        }

        public virtual void PushDown()
        {
        }

        public virtual void PushFire()
        {
        }

        /// <summary>
        /// 選択肢が選択された場合の処理
        /// </summary>
        public virtual void SelectMenu()
        {
        }
    }
}
