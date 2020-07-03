using Microsoft.Xna.Framework;
using System.IO;
using System.Windows.Forms;

namespace Game2.Utilities
{
    /// <summary>
    /// 便利機能
    /// </summary>
    internal static class Utility
    {
        /// <summary>
        /// System.Drawing.Colorのカラー名称をXNAカラーに変換する
        /// </summary>
        /// <param name="name">カラー名称</param>
        /// <returns>XNAカラー</returns>
        internal static Color GetColor(string name)
        {
            System.Drawing.Color sysColor = System.Drawing.Color.FromName(name);
            Color xnaColor = new Color(sysColor.R, sysColor.G, sysColor.B);
            return xnaColor;
        }

        /// <summary>
        /// セーブデータのパスを取得する。末尾に\はない。
        /// </summary>
        /// <returns>セーブデータのパス</returns>
        internal static string GetSaveFilePath()
        {
            // パスを取得
            string path = Application.UserAppDataPath;

            // パスのフォルダを作成
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        internal static bool IsVista()
        {
            System.OperatingSystem os = System.Environment.OSVersion;

            if (os.Version.Major == 6 && os.Version.Minor == 0)
            {
                return true;
            }

            return false;
        }
    }
}