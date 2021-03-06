using Microsoft.Xna.Framework.Graphics;

namespace Game2.Screens
{
    /// <summary>
    /// ストーリー画面
    /// </summary>
    internal class StoryScreen : EndingScreen
    {
        internal StoryScreen(ref Game2 game2, ref SpriteFont font) : base(ref game2, ref font)
        {
            AlwaysSkip = true;
        }

        internal override void AddSecondMsg()
        {
        }

        internal override void AddThirdMsg()
        {
        }

        internal override string FileName()
        {
            return "Story";
        }

        internal override int NumOfImage()
        {
            return 4;
        }

        internal override string Msg1()
        {
            return "Story";
        }

        internal override float Msg1Scale()
        {
            return 1f;
        }

        internal override string Msg2()
        {
            return "Good luck!";
        }

        internal override float Msg2Scale()
        {
            return 1f;
        }

        internal override float WaitTime1()
        {
            return 1000f;
        }

        internal override float WaitTime2()
        {
            return 3000f;
        }

        internal override string BgmName()
        {
            return "Songs/BGM1";
        }
    }
}
