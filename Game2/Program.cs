using System;
using System.Threading;

namespace Game2
{
    /// <summary>
    /// The main class.
    /// </summary>
    internal static class Program
    {
        //二重起動防止
        private static readonly Mutex _mutex = new Mutex(false, "GAME2");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            if (!_mutex.WaitOne(0, false))
            {
                return;
            }

            using (Game2 game = new Game2())
            {
                game.Run();
            }
        }
    }
}
