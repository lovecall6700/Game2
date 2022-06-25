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
        private ButtonStatus _fire = ButtonStatus.Release;
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
            UpdateStatus(Fire, ref _fire);
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
#pragma warning disable IDE0066 // switch ステートメントを式に変換します
            switch (name)
#pragma warning restore IDE0066 // switch ステートメントを式に変換します
            {
                case ButtonNames.Jump:
                    return _jump == ButtonStatus.Release;
                case ButtonNames.Fire:
                    return _fire == ButtonStatus.Release;
                case ButtonNames.Left:
                    return _left == ButtonStatus.Release;
                case ButtonNames.Right:
                    return _right == ButtonStatus.Release;
                case ButtonNames.Down:
                    return _down == ButtonStatus.Release;
                case ButtonNames.Up:
                    return _up == ButtonStatus.Release;
                case ButtonNames.Pause:
                    return _pause == ButtonStatus.Release;
                case ButtonNames.FullScreen:
                    return _fullScreen == ButtonStatus.Release;
                case ButtonNames.Screenshot:
                    return _screenshot == ButtonStatus.Release;
                case ButtonNames.Exit:
                    return _exit == ButtonStatus.Release;
            }

            return false;
        }

        /// <summary>
        /// ボタンが押下か返す。
        /// </summary>
        /// <param name="name">KeyName</param>
        /// <returns>ボタンが押下か</returns>
        internal bool IsPress(ButtonNames name)
        {
#pragma warning disable IDE0066 // switch ステートメントを式に変換します
            switch (name)
#pragma warning restore IDE0066 // switch ステートメントを式に変換します
            {
                case ButtonNames.Jump:
                    return _jump == ButtonStatus.Press;
                case ButtonNames.Fire:
                    return _fire == ButtonStatus.Press;
                case ButtonNames.Left:
                    return _left == ButtonStatus.Press;
                case ButtonNames.Right:
                    return _right == ButtonStatus.Press;
                case ButtonNames.Down:
                    return _down == ButtonStatus.Press;
                case ButtonNames.Up:
                    return _up == ButtonStatus.Press;
                case ButtonNames.Pause:
                    return _pause == ButtonStatus.Press;
                case ButtonNames.FullScreen:
                    return _fullScreen == ButtonStatus.Press;
                case ButtonNames.Screenshot:
                    return _screenshot == ButtonStatus.Press;
                case ButtonNames.Exit:
                    return _exit == ButtonStatus.Press;
            }

            return false;
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

                    if (_fire == ButtonStatus.Click)
                    {
                        _fire = ButtonStatus.Release;
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
