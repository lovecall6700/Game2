using Game2.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        /// <summary>
        /// OSがVistaか調べる
        /// </summary>
        /// <returns>OSがVistaか</returns>
        internal static bool IsVista()
        {
            System.OperatingSystem os = System.Environment.OSVersion;

            if (os.Version.Major == 6 && os.Version.Minor == 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 対象を追尾するための速度ベクトルを得る
        /// </summary>
        /// <param name="obj">追跡者</param>
        /// <param name="target">対象座標</param>
        /// <param name="velocity">速度ベクトル</param>
        /// <param name="speed">速度</param>
        internal static void Homing(PhysicsObject obj, Vector2 target, ref Vector2 velocity, float speed)
        {

            if (target != obj.Position)
            {
                Vector2 v = Vector2.Normalize(target - obj.Position) * speed;

                //左右向きプルプルをやめる
                if (Vector2.Distance(target, obj.Position) <= v.Length())
                {
                    velocity = target - obj.Position;
                }
                else
                {
                    velocity = v;

                    if (velocity.X > 0)
                    {
                        obj.ControlDirectionX = 1;
                    }
                    else if (velocity.X < 0)
                    {
                        obj.ControlDirectionX = -1;
                    }
                }
            }
        }

        /// <summary>
        /// 指定フォントにおける文字列の描画サイズを得る
        /// </summary>
        /// <param name="font">SpriteFont</param>
        /// <param name="msg">文字列</param>
        /// <param name="scale">表示倍率</param>
        /// <returns>描画サイズ</returns>
        internal static Vector2 GetMsgSize(SpriteFont font, string msg, float scale)
        {
            return font.MeasureString(msg) * scale;
        }
    }
}