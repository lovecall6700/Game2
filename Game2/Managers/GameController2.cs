using Game2.Utilities;
using Microsoft.Xna.Framework;

namespace Game2.Managers
{
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
        private bool _flipflop = false;
        private readonly Timer _timer = new Timer();

        internal float ClickTime = 100f;
        internal float RepeatTime = 100f;

        internal void Update(GameTime gameTime)
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
            else //if(state == ControllerStatus.Repeat)
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
            bool b;

            switch (name)
            {
                case KeyName.Jump:

                    b = _jump == KeyStatus.Click;
                    _jump = KeyStatus.Release;
                    return b;

                case KeyName.Fire:

                    b = _fire == KeyStatus.Click;
                    _jump = KeyStatus.Release;
                    return b;

                case KeyName.Left:

                    b = _left == KeyStatus.Click;
                    _jump = KeyStatus.Release;
                    return b;

                case KeyName.Right:

                    b = _right == KeyStatus.Click;
                    _jump = KeyStatus.Release;
                    return b;

                case KeyName.Down:

                    b = _down == KeyStatus.Click;
                    _jump = KeyStatus.Release;
                    return b;

                case KeyName.Up:

                    b = _up == KeyStatus.Click;
                    _jump = KeyStatus.Release;
                    return b;

                case KeyName.Pause:

                    b = _pause == KeyStatus.Click;
                    _pause = KeyStatus.Release;
                    return b;

                case KeyName.FullScreen:

                    b = _fullScreen == KeyStatus.Click;
                    _fullScreen = KeyStatus.Release;
                    return b;
            }

            return false;
        }
    }
}
