namespace Game2
{
    /// <summary>
    /// 状態変更のスケジュール管理
    /// </summary>
    internal partial class Scheduler
    {
        /// <summary>
        /// 画面の切り替え予約
        /// </summary>
        internal Schedule Next = Schedule.None;

        /// <summary>
        /// ゲームプレイ中か
        /// </summary>
        internal bool Playing;

        private readonly Game2 _game2;

        internal Scheduler(Game2 game2)
        {
            _game2 = game2;
        }

        /// <summary>
        /// エンディング画面を予約
        /// </summary>
        internal void Ending()
        {
            if (Next != Schedule.None)
            {
                return;
            }

            Next = Schedule.Ending;
        }

        /// <summary>
        /// ストーリー画面を予約
        /// </summary>
        internal void Story()
        {
            if (Next != Schedule.None)
            {
                return;
            }

            Next = Schedule.Story;
        }

        /// <summary>
        /// リトライを予約
        /// </summary>
        internal void Retry()
        {
            if (Next != Schedule.None)
            {
                return;
            }

            Next = Schedule.Retry;
        }

        /// <summary>
        /// タイトル画面を予約
        /// </summary>
        internal void Title()
        {
            if (Next != Schedule.None)
            {
                return;
            }

            Next = Schedule.Title;
        }

        /// <summary>
        /// 最初から開始を予約
        /// </summary>
        internal void InitialStart()
        {
            if (Next != Schedule.None)
            {
                return;
            }

            Next = Schedule.InitialStart;
        }

        /// <summary>
        /// リスタートかゲームオーバーの判定を予約
        /// </summary>
        internal void RestartOrGameover()
        {
            if (Next != Schedule.None)
            {
                return;
            }

            Next = Schedule.RestartOrGameover;
        }

        /// <summary>
        /// ステージ開始画面を予約
        /// </summary>
        internal void GameStart()
        {
            if (Next != Schedule.None)
            {
                return;
            }

            Next = Schedule.GameStart;
        }

        /// <summary>
        /// 終了を予約
        /// </summary>
        internal void Quit()
        {
            if (Next != Schedule.None)
            {
                return;
            }

            Next = Schedule.Quit;
        }

        /// <summary>
        /// コンティニュースタートを予約
        /// </summary>
        internal void ContinueStart()
        {
            if (Next != Schedule.None)
            {
                return;
            }

            Next = Schedule.ContinueStart;
        }

        /// <summary>
        /// BGM音量変更画面を予約
        /// </summary>
        internal void BGMVolume()
        {
            if (Next != Schedule.None)
            {
                return;
            }

            Next = Schedule.BGMVolume;
        }

        /// <summary>
        /// SE音量変更画面を予約
        /// </summary>
        internal void SEVolume()
        {
            if (Next != Schedule.None)
            {
                return;
            }

            Next = Schedule.SEVolume;
        }

        /// <summary>
        /// オプション画面を予約
        /// </summary>
        internal void Options()
        {
            if (Next != Schedule.None)
            {
                return;
            }

            Next = Schedule.Options;
        }

        /// <summary>
        /// セーブを予約
        /// </summary>
        internal void SaveStage()
        {
            if (Next != Schedule.None)
            {
                return;
            }

            Next = Schedule.SaveStage;
        }

        /// <summary>
        /// 扉に入った
        /// </summary>
        internal void EnterDoor()
        {
            if (Next != Schedule.None)
            {
                return;
            }

            Next = Schedule.EnterDoor;
        }

        /// <summary>
        /// 予約した処理を実際に実行する
        /// </summary>
        internal void Update()
        {
            if (Next == Schedule.None)
            {
                return;
            }

            //次画面の予約を確認して移動する
            switch (Next)
            {
                case Schedule.EnterDoor:

                    Playing = false;
                    _game2.ExecEnterDoor();
                    break;

                case Schedule.RestartOrGameover:

                    Playing = false;
                    _game2.ExecRestartOrGameover();
                    break;

                case Schedule.Ending:

                    Playing = false;
                    _game2.ExecEnding();
                    break;

                case Schedule.GameStart:

                    Playing = true;
                    _game2.ExecGameStart();
                    break;

                case Schedule.Title:

                    Playing = false;
                    _game2.ExecTitle();
                    break;

                case Schedule.Retry:

                    Playing = false;
                    _game2.ExecRetry();
                    break;

                case Schedule.Quit:

                    Playing = false;
                    _game2.ExecQuit();
                    return;

                case Schedule.ContinueStart:

                    Playing = false;
                    _game2.ExecContinueStart();
                    break;

                case Schedule.InitialStart:

                    Playing = false;
                    _game2.ExecInitialStart();
                    break;

                case Schedule.SaveStage:

                    Playing = false;
                    _game2.ExecSaveStage();
                    break;

                case Schedule.BGMVolume:

                    Playing = false;
                    _game2.ExecBGMVolume();
                    break;

                case Schedule.SEVolume:

                    Playing = false;
                    _game2.ExecSEVolume();
                    break;

                case Schedule.Options:

                    Playing = false;
                    _game2.ExecOptions();
                    break;

                case Schedule.Story:

                    Playing = false;
                    _game2.ExecStory();
                    break;
            }

            Next = Schedule.None;
        }
    }
}
