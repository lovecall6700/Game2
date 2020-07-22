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

        /// <summary>
        /// 画面が出てからしばらくは操作できない
        /// </summary>
        internal Timer WaitTimer = new Timer();

        /// <summary>
        /// ボタンが押しっぱなし
        /// </summary>
        internal bool Press = true;

        internal SelectScreen(Game2 game2, SpriteFont font) : base(game2)
        {
            Game2 = game2;
            Font = font;
            WaitTimer.Start(250f, true);
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
            //画面が出てからしばらくは操作できない
            if (WaitTimer.Update(ref gameTime))
            {
                return;
            }

            bool p = false;

            if (Game2.GameCtrl.Up)
            {
                p = true;

                if (!Press)
                {
                    //前回押されていないときに上が押された
                    Index = MathHelper.Clamp(Index - 1, 0, Items.Count - 1);

                    if (Items[Index].Disable)
                    {
                        Index = MathHelper.Clamp(Index - 1, 0, Items.Count - 1);
                    }

                    Game2.MusicPlayer.PlaySE("SoundEffects/MenuChange");
                }
            }
            else if (Game2.GameCtrl.Down)
            {
                p = true;

                if (!Press)
                {
                    //前回押されていないときに下が押された
                    Index = MathHelper.Clamp(Index + 1, 0, Items.Count - 1);

                    //連続で無効は想定していない
                    if (Items[Index].Disable)
                    {
                        Index = MathHelper.Clamp(Index + 1, 0, Items.Count - 1);
                    }

                    Game2.MusicPlayer.PlaySE("SoundEffects/MenuChange");
                }
            }
            else if (Game2.GameCtrl.Fire)
            {
                p = true;

                if (!Press)
                {
                    //前回押されていないときに選択が押された
                    SelectMenu();
                    Game2.MusicPlayer.PlaySE("SoundEffects/MenuSelect");
                }
            }

            Press = p;
        }

        /// <summary>
        /// 選択肢が選択された場合の処理
        /// </summary>
        internal virtual void SelectMenu()
        {
        }
    }
}
