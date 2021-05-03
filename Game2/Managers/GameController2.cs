using Game2.Utilities;
using Microsoft.Xna.Framework;

namespace Game2.Managers
{
    internal class GameController2 : GameController
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
        /// 連打時のボタン状態
        /// </summary>
        private bool _flipflop = false;

        /// <summary>
        /// クリックの制限時間
        /// </summary>
        internal float ClickTime = 300f;

        /// <summary>
        /// 連打の制限時間
        /// </summary>
        internal float RepeatTime = 250f;

        internal void Update(ref GameTime gameTime)
        {
            base.Update();
            _timer.Update(ref gameTime);
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
                    _timer.Start(ClickTime, true);
                }
            }
            else if (state == ButtonStatus.Press)
            {
                //プレス時
                if (!raw)
                {
                    if (_timer.Running)
                    {
                        //一定時間以内に離れたらクリックへ
                        state = ButtonStatus.Click;
                        _timer.Start(RepeatTime, true);
                    }
                    else
                    {
                        //一定時間離れたままならリリースへ
                        state = ButtonStatus.Release;
                    }
                }
            }
            else if (state == ButtonStatus.Click || state == ButtonStatus.Click2)
            {
                //クリック時
                if (_timer.Running)
                {
                    if (raw)
                    {
                        //一定時間内に押されたらリピートへ
                        state = ButtonStatus.Repeat;
                        _flipflop = true;
                        _timer.Start(RepeatTime, true);
                    }
                }
                else
                {
                    //一定時間以上離れたらリリースへ
                    state = ButtonStatus.Release;
                }
            }
            else if (state == ButtonStatus.Repeat)
            {
                //リピート時
                if (_timer.Running)
                {
                    //一定時間内に
                    if (_flipflop)
                    {
                        if (!raw)
                        {
                            //押されていたものが離されたらリピートを維持
                            _flipflop = false;
                            _timer.Start(RepeatTime, true);
                        }
                    }
                    else
                    {
                        if (raw)
                        {
                            //押されたらリピートを維持
                            _flipflop = true;
                            _timer.Start(RepeatTime, true);
                        }
                    }
                }
                else
                {
                    //一定時間
                    if (raw)
                    {
                        //押されたままだったらプレスへ
                        state = ButtonStatus.Press;
                    }
                    else
                    {
                        //離れたままだったらリリースへ
                        state = ButtonStatus.Release;
                    }
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
            switch (name)
            {
                case ButtonNames.Jump:

                    return _jump == ButtonStatus.Release || _jump == ButtonStatus.Click2;

                case ButtonNames.Fire:

                    return _fire == ButtonStatus.Release || _fire == ButtonStatus.Click2;

                case ButtonNames.Left:

                    return _left == ButtonStatus.Release || _left == ButtonStatus.Click2;

                case ButtonNames.Right:

                    return _right == ButtonStatus.Release || _right == ButtonStatus.Click2;

                case ButtonNames.Down:

                    return _down == ButtonStatus.Release || _down == ButtonStatus.Click2;

                case ButtonNames.Up:

                    return _up == ButtonStatus.Release || _up == ButtonStatus.Click2;

                case ButtonNames.Pause:

                    return _pause == ButtonStatus.Release || _pause == ButtonStatus.Click2;

                case ButtonNames.FullScreen:

                    return _fullScreen == ButtonStatus.Release || _fullScreen == ButtonStatus.Click2;

                case ButtonNames.Screenshot:

                    return _screenshot == ButtonStatus.Release || _screenshot == ButtonStatus.Click2;

                case ButtonNames.Exit:

                    return _exit == ButtonStatus.Release || _exit == ButtonStatus.Click2;
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
            switch (name)
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
        /// ボタンが連打か返す。
        /// </summary>
        /// <param name="name">KeyName</param>
        /// <returns>ボタンが連打か</returns>
        internal bool IsRepeat(ButtonNames name)
        {
            switch (name)
            {
                case ButtonNames.Jump:

                    return _jump == ButtonStatus.Repeat;

                case ButtonNames.Fire:

                    return _fire == ButtonStatus.Repeat;

                case ButtonNames.Left:

                    return _left == ButtonStatus.Repeat;

                case ButtonNames.Right:

                    return _right == ButtonStatus.Repeat;

                case ButtonNames.Down:

                    return _down == ButtonStatus.Repeat;

                case ButtonNames.Up:

                    return _up == ButtonStatus.Repeat;

                case ButtonNames.Pause:

                    return _pause == ButtonStatus.Repeat;

                case ButtonNames.FullScreen:

                    return _fullScreen == ButtonStatus.Repeat;

                case ButtonNames.Screenshot:

                    return _screenshot == ButtonStatus.Repeat;

                case ButtonNames.Exit:

                    return _exit == ButtonStatus.Repeat;
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
                        _jump = ButtonStatus.Click2;
                        b = true;
                    }

                    return b;

                case ButtonNames.Fire:

                    if (_fire == ButtonStatus.Click)
                    {
                        _fire = ButtonStatus.Click2;
                        b = true;
                    }

                    return b;

                case ButtonNames.Left:

                    if (_left == ButtonStatus.Click)
                    {
                        _left = ButtonStatus.Click2;
                        b = true;
                    }

                    return b;

                case ButtonNames.Right:

                    if (_right == ButtonStatus.Click)
                    {
                        _right = ButtonStatus.Click2;
                        b = true;
                    }

                    return b;

                case ButtonNames.Down:

                    if (_down == ButtonStatus.Click)
                    {
                        _down = ButtonStatus.Click2;
                        b = true;
                    }

                    return b;

                case ButtonNames.Up:

                    if (_up == ButtonStatus.Click)
                    {
                        _up = ButtonStatus.Click2;
                        b = true;
                    }

                    return b;

                case ButtonNames.Pause:

                    if (_pause == ButtonStatus.Click)
                    {
                        _pause = ButtonStatus.Click2;
                        b = true;
                    }

                    return b;

                case ButtonNames.FullScreen:

                    if (_fullScreen == ButtonStatus.Click)
                    {
                        _fullScreen = ButtonStatus.Click2;
                        b = true;
                    }

                    return b;

                case ButtonNames.Screenshot:

                    if (_screenshot == ButtonStatus.Click)
                    {
                        _screenshot = ButtonStatus.Click2;
                        b = true;
                    }

                    return b;

                case ButtonNames.Exit:

                    if (_exit == ButtonStatus.Click)
                    {
                        _exit = ButtonStatus.Click2;
                        b = true;
                    }

                    return b;
            }

            return false;
        }
    }
}
