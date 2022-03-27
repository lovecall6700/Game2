using Game2.GameObjects;
using Game2.Inputs;
using Game2.Managers;
using Game2.Screens;
using Game2.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace Game2
{
    /// <summary>
    /// Game2
    /// </summary>
    internal class Game2 : Game
    {
        private Game2 _game2;

        //タイマー
        private readonly Timer _fullScTimer = new Timer();
        private readonly Timer _pauseTimer = new Timer();

        //画面構成物
        private ScoreDisplay _scoreDisp;
        private RemainDisplay _remainDisp;
        private TimeLimitDisplay _timeLimitDisp;
        private LifeDisplay _lifeDisp;
        private PauseDisplay _pauseDisp;

        //画像セット
        internal Texture2D Images = null;

        /// <summary>
        /// 現在描画する画面
        /// </summary>
        private Screen _screen;

        /// <summary>
        /// ハイスコア表示を隠すか
        /// </summary>
        private bool _hideHiscore = false;

        /// <summary>
        /// アプリがフォーカスされているか
        /// </summary>
        private bool _focused = true;

        /// <summary>
        /// アプリがポーズされているか
        /// </summary>
        private bool _paused = false;

        /// <summary>
        /// 状態変更のスケジュール管理
        /// </summary>
        internal Scheduler Scheduler;

        /// <summary>
        /// アイテム管理
        /// </summary>
        internal Inventory Inventory;

        /// <summary>
        /// 音楽プレーヤー
        /// </summary>
        internal MusicPlayer MusicPlayer;

        /// <summary>
        /// ゲームコントローラ
        /// </summary>
        internal GameController2 GameCtrl;

        /// <summary>
        /// カメラ
        /// </summary>
        internal Camera2D Camera2D;

        /// <summary>
        /// 画面間で共有される情報
        /// </summary>
        internal Session Session;

        /// <summary>
        /// ゲームプレイ画面
        /// </summary>
        internal PlayScreen PlaySc;

        /// <summary>
        /// テクスチャ管理
        /// </summary>
        internal Textures Textures;

        /// <summary>
        /// 全体マップに対する描画位置
        /// </summary>
        internal Vector2 Offset;

        //その他のシステム周り
        internal SpriteFont Font;
        internal GraphicsDeviceManager Graphics;
        internal SpriteBatch SpriteBatch;
        private Rectangle _oldLocation;

        /// <summary>
        /// ゲーム開始時のステージ番号
        /// </summary>
        internal static readonly int StartStageNo = 1;

        /// <summary>
        /// ゲーム開始時のドア番号
        /// </summary>
        internal static readonly int StartDoorNo = 3;

        /// <summary>
        /// 隠し扉発見得点
        /// </summary>
        internal static readonly int FindBonus = 1000;

        /// <summary>
        /// ゲーム画面の幅
        /// </summary>
        internal static readonly int Width = 256;

        /// <summary>
        /// ゲーム画面の高さ
        /// </summary>
        internal static readonly int Height = 256;

        /// <summary>
        /// ウィンドウの幅
        /// </summary>
        internal static readonly int WindowWidth = 800;

        /// <summary>
        /// ウィンドウの高さ
        /// </summary>
        internal static readonly int WindowHeight = 600;

        private bool _initCamera2D = false;

        internal Game2()
        {

            Content.RootDirectory = "Content";
            Window.Title = "Game2";

            //フレームレート
            TargetElapsedTime = TimeSpan.FromMilliseconds(1000d / 30d);
            InactiveSleepTime = TimeSpan.FromMilliseconds(1000d);

            //ウィンドウのサイズを確定する
            Camera2D = new Camera2D();
            Graphics = new GraphicsDeviceManager(this);
            _game2 = this;
        }

        protected override void Initialize()
        {
            //ゲームシステム
            UnsetFullscreen();
            Textures = new Textures();
            Scheduler = new Scheduler(ref _game2);
            GameCtrl = new GameController2();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //描画関連
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Images = Content.Load<Texture2D>("Images");
            Font = Content.Load<SpriteFont>("Fonts");
            _timeLimitDisp = new TimeLimitDisplay(ref _game2, ref Font, GraphicsDevice);
            _scoreDisp = new ScoreDisplay(ref _game2, ref Font, GraphicsDevice);
            _remainDisp = new RemainDisplay(ref _game2, ref Font, GraphicsDevice);
            _pauseDisp = new PauseDisplay(ref _game2, ref Font, GraphicsDevice);
            _lifeDisp = new LifeDisplay(ref _game2, ref Font, GraphicsDevice);

            //ゲーム関連
            Inventory = new Inventory(ref _game2);

            //音は最後
            MusicPlayer = new MusicPlayer(Content);

            //すべての初期化後はタイトル画面を予約
            Scheduler.SetSchedule(Schedules.Title);
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            Scheduler.Update();
            GameCtrl.Update(ref gameTime);

            //ウィンドウ位置が変更されたらCamera2Dを初期化する
            Rectangle location = Window.ClientBounds;
            if (location != _oldLocation)
            {
                _oldLocation = location;
                _initCamera2D = true;
            }

            if (_initCamera2D)
            {
                Camera2D.Initialize(GraphicsDevice, Width, Height);
                _initCamera2D = false;
            }

            //ESCで終了
            if (GameCtrl.IsClick(ButtonNames.Exit))
            {
                Scheduler.SetSchedule(Schedules.Title);
                base.Update(gameTime);
                return;
            }

            //Alt+Enterで最大化
            if (!_fullScTimer.Update(ref gameTime) && GameCtrl.IsClick(ButtonNames.FullScreen))
            {
                _paused = true;
                MusicPlayer.StopSong();

                if (Graphics.IsFullScreen)
                {
                    UnsetFullscreen();
                }
                else
                {
                    SetFullscreen();
                }

                _fullScTimer.Start(3000f, true);
                base.Update(gameTime);
                return;
            }

            //スクリーンショット
            if (GameCtrl.IsClick(ButtonNames.Screenshot))
            {
                SaveScreenshot();
            }

            //ポーズ切り替え
            if (!_pauseTimer.Update(ref gameTime) && GameCtrl.IsClick(ButtonNames.Pause))
            {
                _paused = !_paused;

                if (_paused)
                {
                    MusicPlayer.StopSong();
                }
                else
                {
                    MusicPlayer.ReplaySong();
                }

                _pauseTimer.Start(500f, true);
            }

            if (!_focused || _paused)
            {
                _pauseDisp.Update(ref gameTime);
                base.Update(gameTime);
                return;
            }

            if (Scheduler.Playing)
            {
                _scoreDisp.Update(ref gameTime);
                _remainDisp.Update(ref gameTime);
                _timeLimitDisp.Update(ref gameTime);
                _lifeDisp.Update(ref gameTime);
            }

            _screen.Update(ref Offset, ref gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// キャラクターが上下方向の画面外に出たか確認する
        /// </summary>
        /// <param name="y">キャラクターのY座標</param>
        /// <returns>画面外に出たか</returns>
        internal bool IsOutOfMapY(float y)
        {
            return y > PlaySc.OutOfMapY || y < -100;
        }

        /// <summary>
        /// 得点を加算する
        /// </summary>
        /// <param name="score">得点</param>
        internal void AddScore(int score)
        {
            int s = score;

            if (Inventory.HasDoubleScoreItem())
            {
                s *= 2;
            }

            _scoreDisp.AddScore(s);
            _remainDisp.AddScore(s);
            Session.UpdateHighScore(_scoreDisp.Value);
        }

        /// <summary>
        /// 得点を返す
        /// </summary>
        /// <returns>得点</returns>
        internal int GetScore()
        {
            return _scoreDisp.Value;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (Scheduler.Playing)
            {
                //ゲーム描画
                GraphicsDevice.Clear(PlaySc.GetBackColor());
                SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Camera2D.Transform);
                PlaySc.Draw(ref Offset, ref gameTime, ref SpriteBatch);
                _timeLimitDisp.Draw(ref SpriteBatch);
                _lifeDisp.Draw(ref SpriteBatch);
                _remainDisp.Draw(ref SpriteBatch);
                _scoreDisp.Draw(ref SpriteBatch);

                if (!_focused || _paused)
                {
                    _pauseDisp.Draw(ref SpriteBatch);
                }

                SpriteBatch.End();
            }
            else
            {
                GraphicsDevice.Clear(Color.Black);
                SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Camera2D.Transform);
                _screen.Draw(ref Offset, ref gameTime, ref SpriteBatch);

                if (!_hideHiscore)
                {
                    _scoreDisp.DrawHighScore(ref SpriteBatch);
                }

                if (!_focused || _paused)
                {
                    _pauseDisp.Draw(ref SpriteBatch);
                }

                SpriteBatch.End();
            }

            base.Draw(gameTime);
        }

        /// <summary>
        /// ゲームが起動したら、タイトル画面を表示
        /// </summary>
        internal void ExecTitle()
        {
            Session = new Session();
            _hideHiscore = false;
            _screen = new TitleScreen(ref _game2, ref Font);
        }

        /// <summary>
        /// ゲームを終了する
        /// </summary>
        internal void ExecQuit()
        {
            Exit();
        }

        /// <summary>
        /// コンティニューで続きを遊ぶ
        /// </summary>
        internal void ExecContinueStart()
        {
            Session = new Session
            {
                EnableTime = false
            };
            Session.LoadStage();
            Session.Life = Player.MaxLife;
            _remainDisp.TitleContinue();
            _scoreDisp.TitleToLoadStart();
            Inventory.TitleToLoadStart();
            _screen = new StageStart(ref _game2, ref Font);
            PlaySc = new PlayScreen(ref _game2);
            PlaySc.LoadStage();
        }

        /// <summary>
        /// 最初から遊ぶ
        /// </summary>
        internal void ExecInitialStart()
        {
            _hideHiscore = false;
            Session = new Session();
            Session.StartTime();
            Session.StageNo = StartStageNo;
            Session.DoorNo = StartDoorNo;
            Session.Life = Player.MaxLife;
            _remainDisp.TitleToInitialStart();
            _scoreDisp.TitleToInitialStart();
            Inventory.TitleToInitialStart();
            _screen = new StageStart(ref _game2, ref Font);
            PlaySc = new PlayScreen(ref _game2);
            PlaySc.LoadStage();
        }

        /// <summary>
        /// ゲームを開始する
        /// </summary>
        internal void ExecGameStart()
        {
            _screen = PlaySc;
            PlaySc.GameStart();
            _timeLimitDisp.Timer.Start(Session.TimeLimit, true);
        }

        /// <summary>
        /// ミス時、ステージ開始画面かゲームオーバー画面を表示する
        /// </summary>
        internal void ExecRestartOrGameover()
        {
            if (_remainDisp.Miss())
            {
                SaveHighScore();
                _screen = new GameoverScreen(ref _game2, ref Font);
            }
            else
            {
                _timeLimitDisp.Timer.Start(Session.TimeLimit, true);
                Session.Life = Player.MaxLife;
                PlaySc.Restart();
                _screen = new StageStart(ref _game2, ref Font);
            }
        }

        /// <summary>
        /// エンディング画面を表示する
        /// </summary>
        internal void ExecEnding()
        {
            _hideHiscore = true;

            if (Session == null)
            {
                Session = new Session
                {
                    EnableTime = false
                };
            }

            Session.EndTime();
            SaveHighScore();
            _screen = new EndingScreen(ref _game2, ref Font);
        }

        /// <summary>
        /// 扉に入る
        /// </summary>
        internal void ExecEnterDoor()
        {
            Session.StageNo = Session.DestStageNo;
            Session.DoorNo = Session.DestDoorNo;
            Session.Life = PlaySc.Player.Life;
            PlaySc.LoadStage();
            _screen = new StageStart(ref _game2, ref Font);
        }

        /// <summary>
        /// セーブする
        /// </summary>
        internal void ExecSaveStage()
        {
            Session.SaveStage();
        }

        /// <summary>
        /// ゲームオーバー時にリトライする
        /// </summary>
        internal void ExecRetry()
        {
            _remainDisp.GameoverRetry();
            _scoreDisp.GameoverRetryToStart();
            Inventory.GameoverRetryToStart();
            PlaySc.Restart();
            _screen = new StageStart(ref _game2, ref Font);
        }

        /// <summary>
        /// BGM音量変更画面を表示する
        /// </summary>
        internal void ExecBGMVolume()
        {
            _screen = new BGMVolumeScreen(ref _game2, ref Font);
        }

        /// <summary>
        /// SE音量変更画面を表示する
        /// </summary>
        internal void ExecSEVolume()
        {
            _screen = new SEVolumeScreen(ref _game2, ref Font);
        }

        /// <summary>
        /// オプション画面を表示する
        /// </summary>
        internal void ExecOptions()
        {
            _screen = new OptionsScreen(ref _game2, ref Font);
        }

        /// <summary>
        /// ストーリー画面を表示する
        /// </summary>
        internal void ExecStory()
        {
            _hideHiscore = true;
            _screen = new StoryScreen(ref _game2, ref Font);
        }

        /// <summary>
        /// フォーカスを得た
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="args">EventArgs</param>
        protected override void OnActivated(object sender, EventArgs args)
        {
            base.OnActivated(sender, args);
            _focused = true;
        }

        /// <summary>
        /// フォーカスが外れた
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="args">EventArgs</param>
        protected override void OnDeactivated(object sender, EventArgs args)
        {
            base.OnDeactivated(sender, args);
            _focused = false;
            _paused = true;
            MusicPlayer.StopSong();
        }

        /// <summary>
        /// スクリーンショットを保存する
        /// </summary>
        public void SaveScreenshot()
        {
            try
            {
                //原寸サイズのRenderTargetを作成する
                int width = GraphicsDevice.PresentationParameters.BackBufferWidth;
                int height = GraphicsDevice.PresentationParameters.BackBufferHeight;
                RenderTarget2D screenshot = new RenderTarget2D(GraphicsDevice, width, height, false, SurfaceFormat.Color, DepthFormat.None);

                //RenderTargetを変更する
                GraphicsDevice.SetRenderTarget(screenshot);

                //Viewportを再計算する
                Camera2D.Initialize(GraphicsDevice, Width, Height);

                //RenderTargetに描画する
                Draw(new GameTime());

                //RenderTargetを戻す
                GraphicsDevice.SetRenderTarget(null);
                GraphicsDevice.Present();

                //Viewportを再計算する
                Camera2D.Initialize(GraphicsDevice, Width, Height);

                //ファイルに書き出す
                if (screenshot != null)
                {
                    DateTime datetime = DateTime.Now;
                    string ymdhms = datetime.ToString("yyyyMMddHHmmss");
                    FileStream fs = new FileStream(Path.Combine(Utility.GetSaveFilePath(), $"screenshot_{ymdhms}.png"), FileMode.OpenOrCreate);
                    screenshot.SaveAsPng(fs, width, height);
                    fs.Close();
                    screenshot.Dispose();
                }
            }
            catch
            {
            }
        }

        public void SaveHighScore()
        {
            if (Session != null)
            {
                Session.SaveHighScore();
            }
        }

        private void SetFullscreen()
        {
            Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            Graphics.IsFullScreen = true;
            Graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
            Graphics.ApplyChanges();
            _initCamera2D = true;
        }

        private void UnsetFullscreen()
        {
            Graphics.PreferredBackBufferWidth = WindowWidth;
            Graphics.PreferredBackBufferHeight = WindowHeight;
            Graphics.IsFullScreen = false;
            Graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
            Graphics.ApplyChanges();
            _initCamera2D = true;
        }
    }
}
