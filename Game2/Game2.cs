﻿using Game2.GameObjects;
using Game2.Managers;
using Game2.Screens;
using Game2.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game2
{
    /// <summary>
    /// Game2
    /// </summary>
    internal class Game2 : Game
    {
        //タイマー
        private readonly Timer _fullScTimer = new Timer();
        private readonly Timer _pauseTimer = new Timer();

        //画面構成物
        private readonly ScoreDisplay _scoreDisp;
        private readonly RemainDisplay _remainDisp;
        private readonly TimeLimitDisplay _timeLimitDisp;
        private readonly LifeDisplay _lifeDisp;
        private readonly PauseDisplay _pauseDisp;

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
        internal GameController GameCtrl;

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
        internal readonly Textures Textures;

        /// <summary>
        /// 全体マップに対する描画位置
        /// </summary>
        internal Vector2 Offset;

        //その他のシステム周り
        internal Viewport Viewport;
        internal SpriteFont Font;
        internal GraphicsDeviceManager Graphics;
        internal SpriteBatch SpriteBatch;

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

        internal Game2()
        {
            Content.RootDirectory = "Content";
            Window.Title = "Game2";

            //フレームレート
            TargetElapsedTime = TimeSpan.FromMilliseconds(1000d / 30d);
            InactiveSleepTime = TimeSpan.FromMilliseconds(1000d);

            //ウィンドウサイズ等の設定、サイズ変更対応
            Graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = WindowWidth,
                PreferredBackBufferHeight = WindowHeight,
                IsFullScreen = false,
                SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight
            };

            Window.ClientSizeChanged += new EventHandler<EventArgs>(WindowSizeChanged);
            Graphics.ApplyChanges();

            //描画関連
            UpdateViewport();
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Font = Content.Load<SpriteFont>("Fonts/PixelMplus10");

            //ゲームシステム
            Scheduler = new Scheduler(this);
            Scheduler.Title();
            Session = new Session();
            Textures = new Textures(Content);
            _timeLimitDisp = new TimeLimitDisplay(this, Font, GraphicsDevice);
            _scoreDisp = new ScoreDisplay(this, Font, GraphicsDevice);
            _remainDisp = new RemainDisplay(this, Font, GraphicsDevice);
            _pauseDisp = new PauseDisplay(this, Font, GraphicsDevice);
            _lifeDisp = new LifeDisplay(this, Font, GraphicsDevice);
            Inventory = new Inventory(this);
            MusicPlayer = new MusicPlayer(Content);
            GameCtrl = new GameController();
            Camera2D = new Camera2D();
            Camera2D.Initialize(GraphicsDevice, Width, Height);
        }

        /// <summary>
        /// ウィンドウサイズ変更時の処理
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void WindowSizeChanged(object sender, EventArgs e)
        {
            if (Graphics.IsFullScreen)
            {
                Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            }
            else
            {
                Graphics.PreferredBackBufferWidth = WindowWidth;
                Graphics.PreferredBackBufferHeight = WindowHeight;
            }

            Graphics.ApplyChanges();
            UpdateViewport();
        }

        /// <summary>
        /// Viewportを更新する
        /// </summary>
        private void UpdateViewport()
        {
            int backWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
            int backHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;
            Viewport = new Viewport(0, 0, backWidth, backHeight);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            Scheduler.Update();
            GameCtrl.Update();

            //ESCで終了
            if (GameCtrl.Exit)
            {
                Exit();
            }

            _fullScTimer.Update(ref gameTime);

            //Alt+Enterで最大化
            if (!_fullScTimer.Running && GameCtrl.FullScreen)
            {
                Graphics.ToggleFullScreen();
                _timeLimitDisp.Initialize(GraphicsDevice);
                _scoreDisp.Initialize(GraphicsDevice);
                _remainDisp.Initialize(GraphicsDevice);
                _lifeDisp.Initialize(GraphicsDevice);
                _fullScTimer.Start(3000f, true);
                Camera2D.Initialize(GraphicsDevice, Width, Height);
            }

            _pauseTimer.Update(ref gameTime);

            if (!_pauseTimer.Running && GameCtrl.Pause)
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
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Viewport = Camera2D.Viewport;

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
            _screen = new TitleScreen(this, Font);
            Session.LoadHighScore();
        }

        /// <summary>
        /// ゲームを終了する
        /// </summary>
        internal void ExecQuit()
        {
            Session.SaveHighScore();
            Exit();
        }

        /// <summary>
        /// コンティニューで続きを遊ぶ
        /// </summary>
        internal void ExecContinueStart()
        {
            Session.LoadStage();
            Session.Life = Player.MaxLife;
            _remainDisp.TitleContinue();
            _scoreDisp.TitleToLoadStart();
            Inventory.TitleToLoadStart();
            _screen = new StageStart(this, Font);
            PlaySc = new PlayScreen(this);
            PlaySc.LoadStage();
        }

        /// <summary>
        /// 最初から遊ぶ
        /// </summary>
        internal void ExecInitialStart()
        {
            _hideHiscore = false;
            Session = new Session();
            Session.LoadHighScore();
            Session.StageNo = StartStageNo;
            Session.DoorNo = StartDoorNo;
            Session.Life = Player.MaxLife;
            _remainDisp.TitleToInitialStart();
            _scoreDisp.TitleToInitialStart();
            Inventory.TitleToInitialStart();
            _screen = new StageStart(this, Font);
            PlaySc = new PlayScreen(this);
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
                _screen = new GameoverScreen(this, Font);
            }
            else
            {
                _timeLimitDisp.Timer.Start(Session.TimeLimit, true);
                Session.Life = Player.MaxLife;
                PlaySc.Restart();
                _screen = new StageStart(this, Font);
            }
        }

        /// <summary>
        /// エンディング画面を表示する
        /// </summary>
        internal void ExecEnding()
        {
            _hideHiscore = true;
            _screen = new EndScreen(this, Font);
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
            _screen = new StageStart(this, Font);
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
            _screen = new StageStart(this, Font);
        }

        /// <summary>
        /// BGM音量変更画面を表示する
        /// </summary>
        internal void ExecBGMVolume()
        {
            _screen = new BGMVolumeScreen(this, Font);
        }

        /// <summary>
        /// SE音量変更画面を表示する
        /// </summary>
        internal void ExecSEVolume()
        {
            _screen = new SEVolumeScreen(this, Font);
        }

        /// <summary>
        /// オプション画面を表示する
        /// </summary>
        internal void ExecOptions()
        {
            _screen = new OptionsScreen(this, Font);
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
            MusicPlayer.ReplaySong();
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
    }
}