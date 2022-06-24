using Game2.Utilities;
using Microsoft.Xna.Framework;

namespace Game2.Inputs
{
    /// <summary>
    /// より詳細にプレーヤーの操作をまとめる
    /// 連打なしバージョン
    /// </summary>
    internal class GameController3 : GameController
    {
        private ButtonStatus _up = ButtonStatus.Release;
        private ButtonStatus _down = ButtonStatus.Release;
        private ButtonStatus _left = ButtonStatus.Release;
        private ButtonStatus _right = ButtonStatus.Release;
        private ButtonStatus _jump = ButtonStatus.Release;
        private ButtonStatus _shot = ButtonStatus.Release;
        private ButtonStatus _pause = ButtonStatus.Release;
        private ButtonStatus _fullScreen = ButtonStatus.Release;
        private ButtonStatus _screenshot = ButtonStatus.Release;
        private ButtonStatus _exit = ButtonStatus.Release;
        private readonly Timer _timer = new Timer();

        /// <summary>
        /// クリックの制限時間
        /// </summary>
        internal float ClickTime = 50f;

        internal void Update(ref GameTime gametime)
        {
            base.Update();
            _timer.Update(ref gametime);
            UpdateStatus(Up, ref _up);
            UpdateStatus(Down, ref _down);
            UpdateStatus(Left, ref _left);
            UpdateStatus(Right, ref _right);
            UpdateStatus(Jump, ref _jump);
            UpdateStatus(Fire, ref _shot);
            UpdateStatus(Pause, ref _pause);
            UpdateStatus(FullScreen, ref _fullScreen);
            UpdateStatus(Screenshot, ref _screenshot);
            UpdateStatus(Exit, ref _exit);
        }

        /// <summary>
        /// ボタンの状態を更新する
        /// </summary>
        /// <param name="raw">素のボタン状態</param>
        /// <param name="state">ボタンの状態</param>
        private void UpdateStatus(bool raw, ref ButtonStatus state)
        {
            if (state == ButtonStatus.Release)
            {
                if (raw)
                {
                    //リリース時に押されたら無条件でプレスへ
                    state = ButtonStatus.Press;
                }
            }
            else if (state == ButtonStatus.Press)
            {
                //プレス時
                if (!raw)
                {
                    //一定時間以内に離れたらクリックへ
                    state = ButtonStatus.Click;
                    _timer.Start(ClickTime, true);
                }
            }
            else if (state == ButtonStatus.Click)
            {
                if (!_timer.Running)
                {
                    //有効時間が過ぎたらリリースへ
                    state = ButtonStatus.Release;
                }
            }
        }

        /// <summary>
        /// ボタンが解放か返す。
        /// </summary>
        /// <param name="name">KeyName</param>
        /// <returns>ボタンが解放か</returns>
        internal bool IsRelease(ButtonNames name)
        {
            return name switch
            {
                ButtonNames.Jump => _jump == ButtonStatus.Release || _jump == ButtonStatus.Release,
                ButtonNames.Fire => _shot == ButtonStatus.Release || _shot == ButtonStatus.Release,
                ButtonNames.Left => _left == ButtonStatus.Release || _left == ButtonStatus.Release,
                ButtonNames.Right => _right == ButtonStatus.Release || _right == ButtonStatus.Release,
                ButtonNames.Down => _down == ButtonStatus.Release || _down == ButtonStatus.Release,
                ButtonNames.Up => _up == ButtonStatus.Release || _up == ButtonStatus.Release,
                ButtonNames.Pause => _pause == ButtonStatus.Release || _pause == ButtonStatus.Release,
                ButtonNames.FullScreen => _fullScreen == ButtonStatus.Release || _fullScreen == ButtonStatus.Release,
                ButtonNames.Screenshot => _screenshot == ButtonStatus.Release || _screenshot == ButtonStatus.Release,
                ButtonNames.Exit => _exit == ButtonStatus.Release || _exit == ButtonStatus.Release,
                _ => false,
            };
        }

        /// <summary>
        /// ボタンが押下か返す。
        /// </summary>
        /// <param name="name">KeyName</param>
        /// <returns>ボタンが押下か</returns>
        internal bool IsPress(ButtonNames name)
        {
            return name switch
            {
                ButtonNames.Jump => _jump == ButtonStatus.Press,
                ButtonNames.Fire => _shot == ButtonStatus.Press,
                ButtonNames.Left => _left == ButtonStatus.Press,
                ButtonNames.Right => _right == ButtonStatus.Press,
                ButtonNames.Down => _down == ButtonStatus.Press,
                ButtonNames.Up => _up == ButtonStatus.Press,
                ButtonNames.Pause => _pause == ButtonStatus.Press,
                ButtonNames.FullScreen => _fullScreen == ButtonStatus.Press,
                ButtonNames.Screenshot => _screenshot == ButtonStatus.Press,
                ButtonNames.Exit => _exit == ButtonStatus.Press,
                _ => false,
            };
        }

        /// <summary>
        /// ボタンがクリックか返す。この処理は例外的に状態がリリースに戻される。
        /// </summary>
        /// <param name="name">KeyName</param>
        /// <returns>ボタンがクリックか</returns>
        internal bool IsClick(ButtonNames name)
        {
            bool b = false;

            switch (name)
            {
                case ButtonNames.Jump:

                    if (_jump == ButtonStatus.Click)
                    {
                        _jump = ButtonStatus.Release;
                        b = true;
                    }

                    return b;

                case ButtonNames.Fire:

                    if (_shot == ButtonStatus.Click)
                    {
                        _shot = ButtonStatus.Release;
                        b = true;
                    }

                    return b;

                case ButtonNames.Left:

                    if (_left == ButtonStatus.Click)
                    {
                        _left = ButtonStatus.Release;
                        b = true;
                    }

                    return b;

                case ButtonNames.Right:

                    if (_right == ButtonStatus.Click)
                    {
                        _right = ButtonStatus.Release;
                        b = true;
                    }

                    return b;

                case ButtonNames.Down:

                    if (_down == ButtonStatus.Click)
                    {
                        _down = ButtonStatus.Release;
                        b = true;
                    }

                    return b;

                case ButtonNames.Up:

                    if (_up == ButtonStatus.Click)
                    {
                        _up = ButtonStatus.Release;
                        b = true;
                    }

                    return b;

                case ButtonNames.Pause:

                    if (_pause == ButtonStatus.Click)
                    {
                        _pause = ButtonStatus.Release;
                        b = true;
                    }

                    return b;

                case ButtonNames.FullScreen:

                    if (_fullScreen == ButtonStatus.Click)
                    {
                        _fullScreen = ButtonStatus.Release;
                        b = true;
                    }

                    return b;

                case ButtonNames.Screenshot:

                    if (_screenshot == ButtonStatus.Click)
                    {
                        _screenshot = ButtonStatus.Release;
                        b = true;
                    }

                    return b;

                case ButtonNames.Exit:

                    if (_exit == ButtonStatus.Click)
                    {
                        _exit = ButtonStatus.Release;
                        b = true;
                    }

                    return b;
            }

            return false;
        }
    }
}
