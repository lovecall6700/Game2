using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Game2
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
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

            //作業ディレクトリは実行ファイルの場所
            string location = Assembly.GetExecutingAssembly().Location;
            string parent = Directory.GetParent(location).FullName;
            Directory.SetCurrentDirectory(parent);

#pragma warning disable IDE0063 // 単純な 'using' ステートメントを使用する
            using (Game2 game = new Game2())
#pragma warning restore IDE0063 // 単純な 'using' ステートメントを使用する
            {
                game.Run();
            }
        }
    }
}
