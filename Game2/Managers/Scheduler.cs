namespace Game2.Managers
{
    /// <summary>
    /// 状態変更のスケジュール管理
    /// </summary>
    internal class Scheduler
    {
        /// <summary>
        /// 画面の切り替え予約
        /// </summary>
        internal Schedules Next = Schedules.None;

        /// <summary>
        /// ゲームプレイ中か
        /// </summary>
        internal bool Playing;

        private readonly Game2 _game2;

        internal Scheduler(ref Game2 game2)
        {
            _game2 = game2;
        }

        /// <summary>
        /// 次画面または次処理を予約
        /// </summary>
        internal void SetSchedule(Schedules schedule)
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
        internal void Update()
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
                    _game2.ExecEnterDoor();
                    break;

                case Schedules.RestartOrGameover:

                    Playing = false;
                    _game2.ExecRestartOrGameover();
                    break;

                case Schedules.Ending:

                    Playing = false;
                    _game2.ExecEnding();
                    break;

                case Schedules.GameStart:

                    Playing = true;
                    _game2.ExecGameStart();
                    break;

                case Schedules.Title:

                    Playing = false;
                    _game2.ExecTitle();
                    break;

                case Schedules.Retry:

                    Playing = false;
                    _game2.ExecRetry();
                    break;

                case Schedules.Quit:

                    Playing = false;
                    _game2.ExecQuit();
                    return;

                case Schedules.ContinueStart:

                    Playing = false;
                    _game2.ExecContinueStart();
                    break;

                case Schedules.InitialStart:

                    Playing = false;
                    _game2.ExecInitialStart();
                    break;

                case Schedules.SaveStage:

                    Playing = false;
                    _game2.ExecSaveStage();
                    break;

                case Schedules.BGMVolume:

                    Playing = false;
                    _game2.ExecBGMVolume();
                    break;

                case Schedules.SEVolume:

                    Playing = false;
                    _game2.ExecSEVolume();
                    break;

                case Schedules.Options:

                    Playing = false;
                    _game2.ExecOptions();
                    break;

                case Schedules.Story:

                    Playing = false;
                    _game2.ExecStory();
                    break;
            }

            Next = Schedules.None;
        }
    }
}
