using Game2.GameObjects;
using Game2.Screens;

namespace Game2.Managers
{
    /// <summary>
    /// 状態変更のスケジュール管理
    /// </summary>
    public class Scheduler
    {
        /// <summary>
        /// 画面の切り替え予約
        /// </summary>
        public Schedules Next = Schedules.None;

        /// <summary>
        /// ゲームプレイ中か
        /// </summary>
        public bool Playing;

        private readonly Game2 _game2;

        public Scheduler(Game2 game2)
        {
            _game2 = game2;
        }

        /// <summary>
        /// 次画面または次処理を予約
        /// </summary>
        public void SetSchedule(Schedules schedule)
        {
            if (Next != Schedules.None)
            {
                return;
            }

            Next = schedule;
        }

        /// <summary>
        /// 予約した処理を実際に実行する
        /// </summary>
        public void Update()
        {
            if (Next == Schedules.None)
            {
                return;
            }

            //次画面の予約を確認して移動する
            switch (Next)
            {
                case Schedules.EnterDoor:

                    Playing = false;
                    ExecEnterDoor();
                    break;

                case Schedules.RestartOrGameover:

                    Playing = false;
                    ExecRestartOrGameover();
                    break;

                case Schedules.Ending:

                    Playing = false;
                    ExecEnding();
                    break;

                case Schedules.GameStart:

                    Playing = true;
                    ExecGameStart();
                    break;

                case Schedules.Title:

                    Playing = false;
                    ExecTitle();
                    break;

                case Schedules.Retry:

                    Playing = false;
                    ExecRetry();
                    break;

                case Schedules.Quit:

                    Playing = false;
                    ExecQuit();
                    return;

                case Schedules.ContinueStart:

                    Playing = false;
                    ExecContinueStart();
                    break;

                case Schedules.InitialStart:

                    Playing = false;
                    ExecInitialStart();
                    break;

                case Schedules.SaveStage:

                    Playing = false;
                    ExecSaveStage();
                    break;

                case Schedules.BGMVolume:

                    Playing = false;
                    ExecBGMVolume();
                    break;

                case Schedules.SEVolume:

                    Playing = false;
                    ExecSEVolume();
                    break;

                case Schedules.Options:

                    Playing = false;
                    ExecOptions();
                    break;

                case Schedules.Story:

                    Playing = false;
                    ExecStory();
                    break;
            }

            Next = Schedules.None;
        }

        /// <summary>
        /// ゲームが起動したら、タイトル画面を表示
        /// </summary>
        private void ExecTitle()
        {
            _game2.Session = new Session();
            _game2.Screen = new TitleScreen(_game2);
        }

        /// <summary>
        /// ゲームを終了する
        /// </summary>
        private void ExecQuit()
        {
            _game2.Exit();
        }

        /// <summary>
        /// コンティニューで続きを遊ぶ
        /// </summary>
        private void ExecContinueStart()
        {
            _game2.Session = new Session
            {
                EnableTime = false
            };
            _game2.Session.LoadStage();
            _game2.Session.Life = Player.MaxLife;
            _game2.Screen = new StageStart(_game2);
            _game2.PlaySc = new PlayScreen(_game2);
            _game2.PlaySc.LoadStage();
        }

        /// <summary>
        /// 最初から遊ぶ
        /// </summary>
        private void ExecInitialStart()
        {
            _game2.Session = new Session();
            _game2.Session.StartTime();
            _game2.Session.StageNo = Session.StartStageNo;
            _game2.Session.DoorNo = Session.StartDoorNo;
            _game2.Session.Life = Player.MaxLife;
            _game2.Screen = new StageStart(_game2);
            _game2.PlaySc = new PlayScreen(_game2);
            _game2.PlaySc.LoadStage();
        }

        /// <summary>
        /// ゲームを開始する
        /// </summary>
        private void ExecGameStart()
        {
            _game2.Screen = _game2.PlaySc;
            _game2.PlaySc.GameStart();
        }

        /// <summary>
        /// ミス時、ステージ開始画面かゲームオーバー画面を表示する
        /// </summary>
        private void ExecRestartOrGameover()
        {
            if (_game2.Session.Miss())
            {
                _game2.Session.SaveHighScore();
                _game2.Screen = new GameoverScreen(_game2);
            }
            else
            {
                _game2.Session.Life = Player.MaxLife;
                _game2.PlaySc.Restart();
                _game2.Screen = new StageStart(_game2);
            }
        }

        /// <summary>
        /// エンディング画面を表示する
        /// </summary>
        private void ExecEnding()
        {
            if (_game2.Session == null)
            {
                _game2.Session = new Session
                {
                    EnableTime = false
                };
            }

            _game2.Session.EndTime();
            _game2.Session.SaveHighScore();
            _game2.Screen = new EndingScreen(_game2);
        }

        /// <summary>
        /// 扉に入る
        /// </summary>
        private void ExecEnterDoor()
        {
            _game2.Session.StageNo = _game2.Session.DestStageNo;
            _game2.Session.DoorNo = _game2.Session.DestDoorNo;
            _game2.Session.Life = _game2.PlaySc.Player.Life;
            _game2.PlaySc.LoadStage();
            _game2.Screen = new StageStart(_game2);
        }

        /// <summary>
        /// セーブする
        /// </summary>
        private void ExecSaveStage()
        {
            _game2.Session.SaveStage();
        }

        /// <summary>
        /// ゲームオーバー時にリトライする
        /// </summary>
        private void ExecRetry()
        {
            _game2.Session.GameoverRetryToStart();
            _game2.PlaySc.Restart();
            _game2.Screen = new StageStart(_game2);
        }

        /// <summary>
        /// BGM音量変更画面を表示する
        /// </summary>
        private void ExecBGMVolume()
        {
            _game2.Screen = new BGMVolumeScreen(_game2);
        }

        /// <summary>
        /// SE音量変更画面を表示する
        /// </summary>
        private void ExecSEVolume()
        {
            _game2.Screen = new SEVolumeScreen(_game2);
        }

        /// <summary>
        /// オプション画面を表示する
        /// </summary>
        private void ExecOptions()
        {
            _game2.Screen = new OptionsScreen(_game2);
        }

        /// <summary>
        /// ストーリー画面を表示する
        /// </summary>
        private void ExecStory()
        {
            _game2.Screen = new StoryScreen(_game2);
        }
    }
}
