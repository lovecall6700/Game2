using Game2.Utilities;
using Microsoft.Xna.Framework;

namespace Game2.Managers
{
    /// <summary>
    /// ボタンの機能名
    /// </summary>
    internal enum KeyName
    {
        Jump = 0,
        Fire,
        Left,
        Right,
        Down,
        Up,
        Pause,
        FullScreen
    }

    /// <summary>
    /// ボタンの状態
    /// </summary>
    internal enum KeyStatus
    {
        /// <summary>
        /// 解放
        /// </summary>
        Release = 0,
        /// <summary>
        /// クリック(押して離す)
        /// </summary>
        Click,
        /// <summary>
        /// 押下
        /// </summary>
        Press,
        /// <summary>
        /// 連打
        /// </summary>
        Repeat
    }

    internal class GameController2 : GameController
    {
        private KeyStatus _up = KeyStatus.Release;
        private KeyStatus _down = KeyStatus.Release;
        private KeyStatus _left = KeyStatus.Release;
        private KeyStatus _right = KeyStatus.Release;
        private KeyStatus _jump = KeyStatus.Release;
        private KeyStatus _fire = KeyStatus.Release;
        private KeyStatus _pause = KeyStatus.Release;
        private KeyStatus _fullScreen = KeyStatus.Release;
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
        }

        /// <summary>
        /// ボタンの状態を更新する
        /// </summary>
        /// <param name="raw">素のボタン状態</param>
        /// <param name="state">ボタンの状態</param>
        private void UpdateStatus(bool raw, ref KeyStatus state)
        {
            if (state == KeyStatus.Release)
            {
                if (raw)
                {
                    //リリース時に押されたら無条件でプレスへ
                    state = KeyStatus.Press;
                    _timer.Start(ClickTime, true);
                }
            }
            else if (state == KeyStatus.Press)
            {
                //プレス時
                if (!raw)
                {
                    if (_timer.Running)
                    {
                        //一定時間以内に離れたらクリックへ
                        state = KeyStatus.Click;
                        _timer.Start(RepeatTime, true);
                    }
                    else
                    {
                        //一定時間離れたままならリリースへ
                        state = KeyStatus.Release;
                    }
                }
            }
            else if (state == KeyStatus.Click)
            {
                //クリック時
                if (_timer.Running)
                {
                    if (raw)
                    {
                        //一定時間内に押されたらリピートへ
                        state = KeyStatus.Repeat;
                        _flipflop = true;
                        _timer.Start(RepeatTime, true);
                    }
                }
                else
                {
                    //一定時間以上離れたらリリースへ
                    state = KeyStatus.Release;
                }
            }
            else if (state == KeyStatus.Repeat)
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
                        state = KeyStatus.Press;
                    }
                    else
                    {
                        //離れたままだったらリリースへ
                        state = KeyStatus.Release;
                    }
                }
            }
        }

        /// <summary>
        /// ボタンが解放か返す。
        /// </summary>
        /// <param name="name">KeyName</param>
        /// <returns>ボタンが解放か</returns>
        internal bool IsRelease(KeyName name)
        {
            switch (name)
            {
                case KeyName.Jump:

                    return _jump == KeyStatus.Release;

                case KeyName.Fire:

                    return _fire == KeyStatus.Release;

                case KeyName.Left:

                    return _left == KeyStatus.Release;

                case KeyName.Right:

                    return _right == KeyStatus.Release;

                case KeyName.Down:

                    return _down == KeyStatus.Release;

                case KeyName.Up:

                    return _up == KeyStatus.Release;

                case KeyName.Pause:

                    return _pause == KeyStatus.Release;

                case KeyName.FullScreen:

                    return _fullScreen == KeyStatus.Release;
            }

            return false;
        }

        /// <summary>
        /// ボタンが押下か返す。
        /// </summary>
        /// <param name="name">KeyName</param>
        /// <returns>ボタンが押下か</returns>
        internal bool IsPress(KeyName name)
        {
            switch (name)
            {
                case KeyName.Jump:

                    return _jump == KeyStatus.Press;

                case KeyName.Fire:

                    return _fire == KeyStatus.Press;

                case KeyName.Left:

                    return _left == KeyStatus.Press;

                case KeyName.Right:

                    return _right == KeyStatus.Press;

                case KeyName.Down:

                    return _down == KeyStatus.Press;

                case KeyName.Up:

                    return _up == KeyStatus.Press;

                case KeyName.Pause:

                    return _pause == KeyStatus.Press;

                case KeyName.FullScreen:

                    return _fullScreen == KeyStatus.Press;
            }

            return false;
        }

        /// <summary>
        /// ボタンが連打か返す。
        /// </summary>
        /// <param name="name">KeyName</param>
        /// <returns>ボタンが連打か</returns>
        internal bool IsRepeat(KeyName name)
        {
            switch (name)
            {
                case KeyName.Jump:

                    return _jump == KeyStatus.Repeat;

                case KeyName.Fire:

                    return _fire == KeyStatus.Repeat;

                case KeyName.Left:

                    return _left == KeyStatus.Repeat;

                case KeyName.Right:

                    return _right == KeyStatus.Repeat;

                case KeyName.Down:

                    return _down == KeyStatus.Repeat;

                case KeyName.Up:

                    return _up == KeyStatus.Repeat;

                case KeyName.Pause:

                    return _pause == KeyStatus.Repeat;

                case KeyName.FullScreen:

                    return _fullScreen == KeyStatus.Repeat;
            }

            return false;
        }

        /// <summary>
        /// ボタンがクリックか返す。この処理は例外的に状態がリリースに戻される。
        /// </summary>
        /// <param name="name">KeyName</param>
        /// <returns>ボタンがクリックか</returns>
        internal bool IsClick(KeyName name)
        {
            bool b = false;

            switch (name)
            {
                case KeyName.Jump:

                    if (_jump == KeyStatus.Click)
                    {
                        _jump = KeyStatus.Release;
                        b = true;
                    }

                    return b;

                case KeyName.Fire:

                    if (_fire == KeyStatus.Click)
                    {
                        _fire = KeyStatus.Release;
                        b = true;
                    }

                    return b;

                case KeyName.Left:

                    if (_left == KeyStatus.Click)
                    {
                        _left = KeyStatus.Release;
                        b = true;
                    }

                    return b;

                case KeyName.Right:

                    if (_right == KeyStatus.Click)
                    {
                        _right = KeyStatus.Release;
                        b = true;
                    }

                    return b;

                case KeyName.Down:

                    if (_down == KeyStatus.Click)
                    {
                        _down = KeyStatus.Release;
                        b = true;
                    }

                    return b;

                case KeyName.Up:

                    if (_up == KeyStatus.Click)
                    {
                        _up = KeyStatus.Release;
                        b = true;
                    }

                    return b;

                case KeyName.Pause:

                    if (_pause == KeyStatus.Click)
                    {
                        _pause = KeyStatus.Release;
                        b = true;
                    }

                    return b;

                case KeyName.FullScreen:

                    if (_fullScreen == KeyStatus.Click)
                    {
                        _fullScreen = KeyStatus.Release;
                        b = true;
                    }

                    return b;
            }

            return false;
        }
    }
}
