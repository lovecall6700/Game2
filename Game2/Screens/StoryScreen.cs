namespace Game2.Screens
{
    /// <summary>
    /// ストーリー画面
    /// </summary>
    public class StoryScreen : EndingScreen
    {
        public StoryScreen(Game2 game2) : base(game2)
        {
            AlwaysSkip = true;
        }

        public override void AddSecondMsg()
        {
        }

        public override void AddThirdMsg()
        {
        }

        public override string FileName()
        {
            return "Story";
        }

        public override int NumOfImage()
        {
            return 4;
        }

        public override string Msg1()
        {
            return "Story";
        }

        public override float Msg1Scale()
        {
            return 1f;
        }

        public override string Msg2()
        {
            return "Good luck!";
        }

        public override float Msg2Scale()
        {
            return 1f;
        }

        public override int WaitTime1()
        {
            return 30;
        }

        public override int WaitTime2()
        {
            return 30;
        }

        public override string BgmName()
        {
            return "Songs/BGM1";
        }
    }
}
