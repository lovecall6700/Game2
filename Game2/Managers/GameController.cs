using Game2.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;

namespace Game2.Managers
{
    /// <summary>
    /// プレーヤーの操作をまとめる
    /// </summary>
    internal class GameController
    {
        //パッドとキーボードの状態
        private GamePadState _pad1;
        private GamePadState _pad2;
        private GamePadState _pad3;
        private GamePadState _pad4;
        private KeyboardState _key;

        //キー定義
        private Buttons _jumpBtn = Buttons.A;
        private Buttons _fireBtn = Buttons.B;
        private Buttons _upBtn = Buttons.DPadUp;
        private Buttons _downBtn = Buttons.DPadDown;
        private Buttons _leftBtn = Buttons.DPadLeft;
        private Buttons _rightBtn = Buttons.DPadRight;
        private Buttons _pauseBtn = Buttons.Back;
        private Keys _jumpKey = Keys.Space;
        private Keys _fireKey = Keys.B;
        private Keys _exitKey = Keys.Escape;
        private Keys _upKey1 = Keys.Up;
        private Keys _downKey1 = Keys.Down;
        private Keys _leftKey1 = Keys.Left;
        private Keys _rightKey1 = Keys.Right;
        private Keys _upKey2 = Keys.W;
        private Keys _downKey2 = Keys.S;
        private Keys _leftKey2 = Keys.A;
        private Keys _rightKey2 = Keys.D;
        private Keys _pauseKey = Keys.P;
        private Keys _screenshotKey = Keys.F12;

        //押されたキー一覧
        internal bool Jump;
        internal bool Fire;
        internal bool Left;
        internal bool Right;
        internal bool Down;
        internal bool Up;
        internal bool Exit;
        internal bool Pause;
        internal bool FullScreen;
        internal bool Screenshot;

        internal GameController()
        {
            Load();
        }

        internal virtual void Update()
        {
            _key = Keyboard.GetState();
            _pad1 = GamePad.GetState(PlayerIndex.One);
            _pad2 = GamePad.GetState(PlayerIndex.Two);
            _pad3 = GamePad.GetState(PlayerIndex.Three);
            _pad4 = GamePad.GetState(PlayerIndex.Four);
            Left = _pad1.IsButtonDown(_leftBtn) || _key.IsKeyDown(_leftKey1) || _key.IsKeyDown(_leftKey2) || _pad2.IsButtonDown(_leftBtn) || _pad3.IsButtonDown(_leftBtn) || _pad4.IsButtonDown(_leftBtn);
            Right = _pad1.IsButtonDown(_rightBtn) || _key.IsKeyDown(_rightKey1) || _key.IsKeyDown(_rightKey2) || _pad2.IsButtonDown(_rightBtn) || _pad3.IsButtonDown(_rightBtn) || _pad4.IsButtonDown(_rightBtn);
            Down = _pad1.IsButtonDown(_downBtn) || _key.IsKeyDown(_downKey1) || _key.IsKeyDown(_downKey2) || _pad2.IsButtonDown(_downBtn) || _pad3.IsButtonDown(_downBtn) || _pad4.IsButtonDown(_downBtn);
            Up = _pad1.IsButtonDown(_upBtn) || _key.IsKeyDown(_upKey1) || _key.IsKeyDown(_upKey2) || _pad2.IsButtonDown(_upBtn) || _pad3.IsButtonDown(_upBtn) || _pad4.IsButtonDown(_upBtn);
            Jump = _pad1.IsButtonDown(_jumpBtn) || _key.IsKeyDown(_jumpKey) || _pad2.IsButtonDown(_jumpBtn) || _pad3.IsButtonDown(_jumpBtn) || _pad4.IsButtonDown(_jumpBtn);
            Fire = _pad1.IsButtonDown(_fireBtn) || _key.IsKeyDown(_fireKey) || _pad2.IsButtonDown(_fireBtn) || _pad3.IsButtonDown(_fireBtn) || _pad4.IsButtonDown(_fireBtn);
            Exit = _key.IsKeyDown(_exitKey);
            Pause = _pad1.IsButtonDown(_pauseBtn) || _key.IsKeyDown(_pauseKey) || _pad2.IsButtonDown(_pauseBtn) || _pad3.IsButtonDown(_pauseBtn) || _pad4.IsButtonDown(_pauseBtn);
            FullScreen = _key.IsKeyDown(Keys.Enter) && (_key.IsKeyDown(Keys.LeftAlt) || _key.IsKeyDown(Keys.RightAlt));
            Screenshot = _key.IsKeyDown(_screenshotKey);
        }

        private void Load()
        {
            try
            {
                foreach (string line in File.ReadLines(Path.Combine(Utility.GetSaveFilePath(), "KeyConfig.txt")))
                {
                    string[] lines = line.Split('=');

                    if (lines.Length != 2)
                    {
                        continue;
                    }

                    if (lines[0].Trim() == "JumpButton")
                    {
                        if (!TryParse(lines[1].Trim(), out _jumpBtn))
                        {
                            _jumpBtn = Buttons.A;
                        }
                    }
                    else if (lines[0].Trim() == "FireButton")
                    {
                        if (!TryParse(lines[1].Trim(), out _fireBtn))
                        {
                            _fireBtn = Buttons.B;
                        }
                    }
                    else if (lines[0].Trim() == "PauseButton")
                    {
                        if (!TryParse(lines[1].Trim(), out _pauseBtn))
                        {
                            _pauseBtn = Buttons.Back;
                        }
                    }
                    else if (lines[0].Trim() == "UpButton")
                    {
                        if (!TryParse(lines[1].Trim(), out _upBtn))
                        {
                            _upBtn = Buttons.DPadUp;
                        }
                    }
                    else if (lines[0].Trim() == "DownButton")
                    {
                        if (!TryParse(lines[1].Trim(), out _downBtn))
                        {
                            _downBtn = Buttons.DPadDown;
                        }
                    }
                    else if (lines[0].Trim() == "LeftButton")
                    {
                        if (!TryParse(lines[1].Trim(), out _leftBtn))
                        {
                            _leftBtn = Buttons.DPadLeft;
                        }
                    }
                    else if (lines[0].Trim() == "RightButton")
                    {
                        if (!TryParse(lines[1].Trim(), out _rightBtn))
                        {
                            _rightBtn = Buttons.DPadRight;
                        }
                    }
                    else if (lines[0].Trim() == "JumpKey")
                    {
                        if (!TryParse(lines[1].Trim(), out _jumpKey))
                        {
                            _jumpKey = Keys.Space;
                        }
                    }
                    else if (lines[0].Trim() == "FireKey")
                    {
                        if (!TryParse(lines[1].Trim(), out _fireKey))
                        {
                            _fireKey = Keys.B;
                        }
                    }
                    else if (lines[0].Trim() == "ExitKey")
                    {
                        if (!TryParse(lines[1].Trim(), out _exitKey))
                        {
                            _exitKey = Keys.Escape;
                        }
                    }
                    else if (lines[0].Trim() == "UpKey1")
                    {
                        if (!TryParse(lines[1].Trim(), out _upKey1))
                        {
                            _upKey1 = Keys.Up;
                        }
                    }
                    else if (lines[0].Trim() == "DownKey1")
                    {
                        if (!TryParse(lines[1].Trim(), out _downKey1))
                        {
                            _downKey1 = Keys.Down;
                        }
                    }
                    else if (lines[0].Trim() == "LeftKey1")
                    {
                        if (!TryParse(lines[1].Trim(), out _leftKey1))
                        {
                            _leftKey1 = Keys.Left;
                        }
                    }
                    else if (lines[0].Trim() == "RightKey1")
                    {
                        if (!TryParse(lines[1].Trim(), out _rightKey1))
                        {
                            _rightKey1 = Keys.Right;
                        }
                    }
                    else if (lines[0].Trim() == "UpKey2")
                    {
                        if (!TryParse(lines[1].Trim(), out _upKey2))
                        {
                            _upKey2 = Keys.W;
                        }
                    }
                    else if (lines[0].Trim() == "DownKey2")
                    {
                        if (!TryParse(lines[1].Trim(), out _downKey2))
                        {
                            _downKey2 = Keys.S;
                        }
                    }
                    else if (lines[0].Trim() == "LeftKey2")
                    {
                        if (!TryParse(lines[1].Trim(), out _leftKey2))
                        {
                            _leftKey2 = Keys.A;
                        }
                    }
                    else if (lines[0].Trim() == "RightKey2")
                    {
                        if (!TryParse(lines[1].Trim(), out _rightKey2))
                        {
                            _rightKey2 = Keys.D;
                        }
                    }
                    else if (lines[0].Trim() == "PauseKey")
                    {
                        if (!TryParse(lines[1].Trim(), out _pauseKey))
                        {
                            _pauseKey = Keys.P;
                        }
                    }
                    else if (lines[0].Trim() == "ScreenshotKey")
                    {
                        if (!TryParse(lines[1].Trim(), out _screenshotKey))
                        {
                            _screenshotKey = Keys.F12;
                        }
                    }
                }
            }
            catch
            {

            }
        }

        private static bool TryParse<TEnum>(string name, out TEnum button) where TEnum : struct
        {
            return Enum.TryParse(name, out button) && Enum.IsDefined(typeof(TEnum), button);
        }
    }
}
