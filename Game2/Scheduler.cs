namespace Game2
{
    /// <summary>
    /// 状態変更のスケジュール管理
    /// </summary>
    internal class Scheduler
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
        /// 次画面または次処理を予約
        /// </summary>
        internal void SetSchedule(Schedule schedule)
        {
            if (Next != Schedule.None)
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
