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
        /// 連打対応フラグ
        /// </summary>
        private bool _flag = true;

        /// <summary>
        /// 画面が出てからしばらくは操作できない
        /// </summary>
        private readonly Timer _waitTimer = new Timer();

        internal SelectScreen(Game2 game2, SpriteFont font) : base(game2)
        {
            Game2 = game2;
            Font = font;
            _waitTimer.Start(500f, true);
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
            _waitTimer.Update(ref gameTime);

            if (_waitTimer.Running)
            {
                return;
            }

            if (!_flag && Game2.GameCtrl.Up)
            {
                //前回押されていないときに上が押された
                Index = MathHelper.Clamp(Index - 1, 0, Items.Count - 1);

                if (Items[Index].Disable)
                {
                    Index = MathHelper.Clamp(Index - 1, 0, Items.Count - 1);
                }

                _flag = true;
                Game2.MusicPlayer.PlaySE("SoundEffects/MenuChange");
                return;
            }
            else if (_flag && Game2.GameCtrl.Up)
            {
                //連続して上が押されたら無視
                return;
            }

            if (!_flag && Game2.GameCtrl.Down)
            {
                //前回押されていないときに下が押された
                Index = MathHelper.Clamp(Index + 1, 0, Items.Count - 1);

                //連続で無効は想定していない
                if (Items[Index].Disable)
                {
                    Index = MathHelper.Clamp(Index + 1, 0, Items.Count - 1);
                }

                _flag = true;
                Game2.MusicPlayer.PlaySE("SoundEffects/MenuChange");
                return;
            }
            else if (_flag && Game2.GameCtrl.Down)
            {
                return;
            }

            if (!_flag && (Game2.GameCtrl.Fire || Game2.GameCtrl.Jump))
            {
                //前回押されていないときに選択が押された
                SelectMenu();
                _flag = true;
                Game2.MusicPlayer.PlaySE("SoundEffects/MenuSelect");
                return;
            }
            else if (_flag && (Game2.GameCtrl.Fire || Game2.GameCtrl.Jump))
            {
                return;
            }

            _flag = false;
        }

        /// <summary>
        /// 選択肢が選択された場合の処理
        /// </summary>
        internal virtual void SelectMenu()
        {
        }
    }
}
