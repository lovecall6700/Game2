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
    public class Game2 : Game
    {
        private readonly Game2 _game2;

        //タイマー
        private readonly Timer _fullScTimer = new Timer();
        private readonly Timer _pauseTimer = new Timer();

        //画面構成物

        /// <summary>
        /// 得点を画面に表示するか
        /// </summary>
        public ScoreDisplay ScoreDisp;

        /// <summary>
        /// 残機を画面に表示するか
        /// </summary>
        public RemainDisplay RemainDisp;

        /// <summary>
        /// 制限時間を画面に表示するか
        /// </summary>
        public TimeLimitDisplay TimeLimitDisp;

        private LifeDisplay _lifeDisp;
        private PauseDisplay _pauseDisp;

        //画像セット
        public Texture2D Images = null;

        /// <summary>
        /// 現在描画する画面
        /// </summary>
        public Screen Screen;

        /// <summary>
        /// ハイスコア表示を隠すか
        /// </summary>
        public bool HideHiscore = false;

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
        public Scheduler Scheduler;

        /// <summary>
        /// アイテム管理
        /// </summary>
        public Inventory Inventory;

        /// <summary>
        /// 音楽プレーヤー
        /// </summary>
        public MusicPlayer MusicPlayer;

        /// <summary>
        /// ゲームコントローラ
        /// </summary>
        public GameController3 GameCtrl;

        /// <summary>
        /// カメラ
        /// </summary>
        public Camera2D Camera2D;

        /// <summary>
        /// 画面間で共有される情報
        /// </summary>
        public Session Session;

        /// <summary>
        /// ゲームプレイ画面
        /// </summary>
        public PlayScreen PlaySc;

        /// <summary>
        /// テクスチャ管理
        /// </summary>
        public Textures Textures;

        //その他のシステム周り
        public SpriteFont Font;
        public GraphicsDeviceManager Graphics;
        public SpriteBatch SpriteBatch;
        private Rectangle _oldLocation;

        /// <summary>
        /// 隠し扉発見得点
        /// </summary>
        public static readonly int FindBonus = 1000;

        /// <summary>
        /// ゲーム画面の幅
        /// </summary>
        public static readonly int Width = 256;

        /// <summary>
        /// ゲーム画面の高さ
        /// </summary>
        public static readonly int Height = 256;

        /// <summary>
        /// ウィンドウの幅
        /// </summary>
        public static readonly int WindowWidth = 800;

        /// <summary>
        /// ウィンドウの高さ
        /// </summary>
        public static readonly int WindowHeight = 600;

        private bool _initCamera2D = false;

        public float Frame = 33.33333f;

        public Game2()
        {
            //GraphicsDeviceManagerの初期化はこの位置でなければならない。
            //他の位置ではモニタ解像度に対するサイズ補正が動作しない。
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "Game2";
            Window.AllowUserResizing = true;

            //フレームレート
            TargetElapsedTime = TimeSpan.FromMilliseconds(1000d / 30d);
            InactiveSleepTime = TimeSpan.FromMilliseconds(1000d);

            //ウィンドウのサイズを確定する
            Camera2D = new Camera2D();
            _game2 = this;
        }

        protected override void Initialize()
        {
            //ゲームシステム
            UnsetFullscreen();
            Textures = new Textures();
            Scheduler = new Scheduler(_game2);
            GameCtrl = new GameController3();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //描画関連
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Images = Content.Load<Texture2D>("Images");
            Font = Content.Load<SpriteFont>("Fonts");
            TimeLimitDisp = new TimeLimitDisplay(_game2, GraphicsDevice);
            ScoreDisp = new ScoreDisplay(_game2, GraphicsDevice);
            RemainDisp = new RemainDisplay(_game2, GraphicsDevice);
            _pauseDisp = new PauseDisplay(_game2, GraphicsDevice);
            _lifeDisp = new LifeDisplay(_game2, GraphicsDevice);

            //ゲーム関連
            Inventory = new Inventory(_game2);

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
            GameCtrl.Update(gameTime);

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

                if (Scheduler.Playing && PlaySc != null && _paused)
                {
                    PlaySc.FocusCamera2D();
                }
            }

            //ESCで終了
            if (GameCtrl.IsClick(ButtonNames.Exit))
            {
                Scheduler.SetSchedule(Schedules.Title);
                base.Update(gameTime);
                return;
            }

            //Alt+Enterで最大化
            if (!_fullScTimer.Update() && GameCtrl.IsClick(ButtonNames.FullScreen))
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

                _fullScTimer.Start(90);
                base.Update(gameTime);
                return;
            }

            //スクリーンショット
            if (GameCtrl.IsClick(ButtonNames.Screenshot))
            {
                SaveScreenshot();
            }

            //ポーズ切り替え
            if (!_pauseTimer.Update() && GameCtrl.IsClick(ButtonNames.Pause))
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

                _pauseTimer.Start(15);
            }

            if (!_focused || _paused)
            {
                _pauseDisp.Update(gameTime);
                base.Update(gameTime);
                return;
            }

            if (Scheduler.Playing)
            {
                ScoreDisp.Update(gameTime);
                RemainDisp.Update(gameTime);
                TimeLimitDisp.Update(gameTime);
                _lifeDisp.Update(gameTime);
            }

            Screen.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// キャラクターが上下方向の画面外に出たか確認する
        /// </summary>
        /// <param name="y">キャラクターのY座標</param>
        /// <returns>画面外に出たか</returns>
        public bool IsOutOfMapY(float y)
        {
            return y > PlaySc.OutOfMapY || y < -100;
        }

        /// <summary>
        /// 得点を加算する
        /// </summary>
        /// <param name="score">得点</param>
        public void AddScore(int score)
        {
            int s = score;

            if (Inventory.HasDoubleScoreItem())
            {
                s *= 2;
            }

            ScoreDisp.AddScore(s);
            RemainDisp.AddScore(s);
            Session.UpdateHighScore(ScoreDisp.Value);
        }

        /// <summary>
        /// 得点を返す
        /// </summary>
        /// <returns>得点</returns>
        public int GetScore()
        {
            return ScoreDisp.Value;
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
                PlaySc.Draw(gameTime, SpriteBatch);
                TimeLimitDisp.Draw(SpriteBatch);
                _lifeDisp.Draw(SpriteBatch);
                RemainDisp.Draw(SpriteBatch);
                ScoreDisp.Draw(SpriteBatch);

                if (!_focused || _paused)
                {
                    _pauseDisp.Draw(SpriteBatch);
                }

                SpriteBatch.End();
            }
            else
            {
                GraphicsDevice.Clear(Color.Black);
                SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Camera2D.Transform);
                Screen.Draw(gameTime, SpriteBatch);

                if (!HideHiscore)
                {
                    ScoreDisp.DrawHighScore(SpriteBatch);
                }

                if (!_focused || _paused)
                {
                    _pauseDisp.Draw(SpriteBatch);
                }

                SpriteBatch.End();
            }

            base.Draw(gameTime);
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
            FileStream fs = null;
            RenderTarget2D screenshot = null;

            try
            {
                //原寸サイズのRenderTargetを作成する
                int width = GraphicsDevice.PresentationParameters.BackBufferWidth;
                int height = GraphicsDevice.PresentationParameters.BackBufferHeight;
                screenshot = new RenderTarget2D(GraphicsDevice, width, height, false, SurfaceFormat.Color, DepthFormat.None);

                //RenderTargetを変更する
                GraphicsDevice.SetRenderTarget(screenshot);

                //Viewportを再計算する
                Camera2D.Initialize(GraphicsDevice, Width, Height);

                //RenderTargetに描画する
                Draw(new GameTime());

                //ファイルに書き出す
                if (screenshot != null)
                {
                    DateTime datetime = DateTime.Now;
                    string ymdhms = datetime.ToString("yyyyMMddHHmmss");
                    fs = new FileStream(Path.Combine(Utility.GetSaveFilePath(), $"screenshot_{ymdhms}.png"), FileMode.OpenOrCreate);
                    screenshot.SaveAsPng(fs, width, height);
                }
            }
            catch
            {
            }
            finally
            {
                fs?.Close();
                screenshot?.Dispose();

                //RenderTargetを戻す
                GraphicsDevice.SetRenderTarget(null);
                GraphicsDevice.Present();

                //Viewportを再計算する
                Camera2D.Initialize(GraphicsDevice, Width, Height);
            }
        }

        public void SaveHighScore()
        {
            Session?.SaveHighScore();
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
