using Game2.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

namespace Game2.Utilities
{
    /// <summary>
    /// 便利機能
    /// </summary>
    internal static class Utility
    {
        private static readonly Dictionary<string, Color> _color = new Dictionary<string, Color>()
        {
            {"MediumBlue", Color.MediumBlue},
            {"MediumOrchid", Color.MediumOrchid},
            {"MediumPurple", Color.MediumPurple},
            {"MediumSeaGreen", Color.MediumSeaGreen},
            {"MediumSlateBlue", Color.MediumSlateBlue},
            {"MediumSpringGreen", Color.MediumSpringGreen},
            {"MediumTurquoise", Color.MediumTurquoise},
            {"MediumVioletRed", Color.MediumVioletRed},
            {"MonoGameOrange", Color.MonoGameOrange},
            {"MintCream", Color.MintCream},
            {"MistyRose", Color.MistyRose},
            {"Moccasin", Color.Moccasin},
            {"MediumAquamarine", Color.MediumAquamarine},
            {"NavajoWhite", Color.NavajoWhite},
            {"Navy", Color.Navy},
            {"OldLace", Color.OldLace},
            {"MidnightBlue", Color.MidnightBlue},
            {"Maroon", Color.Maroon},
            {"LightYellow", Color.LightYellow},
            {"Linen", Color.Linen},
            {"LawnGreen", Color.LawnGreen},
            {"LemonChiffon", Color.LemonChiffon},
            {"LightBlue", Color.LightBlue},
            {"LightCoral", Color.LightCoral},
            {"LightCyan", Color.LightCyan},
            {"LightGoldenrodYellow", Color.LightGoldenrodYellow},
            {"LightGray", Color.LightGray},
            {"LightGreen", Color.LightGreen},
            {"LightPink", Color.LightPink},
            {"LightSalmon", Color.LightSalmon},
            {"LightSeaGreen", Color.LightSeaGreen},
            {"LightSkyBlue", Color.LightSkyBlue},
            {"LightSlateGray", Color.LightSlateGray},
            {"LightSteelBlue", Color.LightSteelBlue},
            {"Olive", Color.Olive},
            {"Lime", Color.Lime},
            {"LimeGreen", Color.LimeGreen},
            {"Magenta", Color.Magenta},
            {"OliveDrab", Color.OliveDrab},
            {"PaleGreen", Color.PaleGreen},
            {"OrangeRed", Color.OrangeRed},
            {"Silver", Color.Silver},
            {"SkyBlue", Color.SkyBlue},
            {"SlateBlue", Color.SlateBlue},
            {"SlateGray", Color.SlateGray},
            {"Snow", Color.Snow},
            {"SpringGreen", Color.SpringGreen},
            {"SteelBlue", Color.SteelBlue},
            {"Tan", Color.Tan},
            {"Teal", Color.Teal},
            {"Thistle", Color.Thistle},
            {"Tomato", Color.Tomato},
            {"Turquoise", Color.Turquoise},
            {"Violet", Color.Violet},
            {"Wheat", Color.Wheat},
            {"White", Color.White},
            {"WhiteSmoke", Color.WhiteSmoke},
            {"Yellow", Color.Yellow},
            {"Sienna", Color.Sienna},
            {"Orange", Color.Orange},
            {"SeaShell", Color.SeaShell},
            {"SandyBrown", Color.SandyBrown},
            {"Orchid", Color.Orchid},
            {"PaleGoldenrod", Color.PaleGoldenrod},
            {"LavenderBlush", Color.LavenderBlush},
            {"PaleTurquoise", Color.PaleTurquoise},
            {"PaleVioletRed", Color.PaleVioletRed},
            {"PapayaWhip", Color.PapayaWhip},
            {"PeachPuff", Color.PeachPuff},
            {"Peru", Color.Peru},
            {"Pink", Color.Pink},
            {"Plum", Color.Plum},
            {"PowderBlue", Color.PowderBlue},
            {"Purple", Color.Purple},
            {"Red", Color.Red},
            {"RosyBrown", Color.RosyBrown},
            {"RoyalBlue", Color.RoyalBlue},
            {"SaddleBrown", Color.SaddleBrown},
            {"Salmon", Color.Salmon},
            {"SeaGreen", Color.SeaGreen},
            {"Lavender", Color.Lavender},
            {"HotPink", Color.HotPink},
            {"Ivory", Color.Ivory},
            {"DarkGray", Color.DarkGray},
            {"DarkGoldenrod", Color.DarkGoldenrod},
            {"DarkCyan", Color.DarkCyan},
            {"DarkBlue", Color.DarkBlue},
            {"Cyan", Color.Cyan},
            {"Crimson", Color.Crimson},
            {"Cornsilk", Color.Cornsilk},
            {"Khaki", Color.Khaki},
            {"Coral", Color.Coral},
            {"Chocolate", Color.Chocolate},
            {"Chartreuse", Color.Chartreuse},
            {"CadetBlue", Color.CadetBlue},
            {"BurlyWood", Color.BurlyWood},
            {"Brown", Color.Brown},
            {"BlueViolet", Color.BlueViolet},
            {"Blue", Color.Blue},
            {"BlanchedAlmond", Color.BlanchedAlmond},
            {"Black", Color.Black},
            {"Bisque", Color.Bisque},
            {"Beige", Color.Beige},
            {"Azure", Color.Azure},
            {"Aquamarine", Color.Aquamarine},
            {"Aqua", Color.Aqua},
            {"AntiqueWhite", Color.AntiqueWhite},
            {"AliceBlue", Color.AliceBlue},
            {"Transparent", Color.Transparent},
            {"DarkGreen", Color.DarkGreen},
            {"DarkKhaki", Color.DarkKhaki},
            {"CornflowerBlue", Color.CornflowerBlue},
            {"DarkOliveGreen", Color.DarkOliveGreen},
            {"Indigo", Color.Indigo},
            {"IndianRed", Color.IndianRed},
            {"YellowGreen", Color.YellowGreen},
            {"DarkMagenta", Color.DarkMagenta},
            {"GreenYellow", Color.GreenYellow},
            {"Green", Color.Green},
            {"Gray", Color.Gray},
            {"Goldenrod", Color.Goldenrod},
            {"Gold", Color.Gold},
            {"GhostWhite", Color.GhostWhite},
            {"Gainsboro", Color.Gainsboro},
            {"Fuchsia", Color.Fuchsia},
            {"ForestGreen", Color.ForestGreen},
            {"FloralWhite", Color.FloralWhite},
            {"Honeydew", Color.Honeydew},
            {"DodgerBlue", Color.DodgerBlue},
            {"DimGray", Color.DimGray},
            {"DeepSkyBlue", Color.DeepSkyBlue},
            {"DeepPink", Color.DeepPink},
            {"DarkViolet", Color.DarkViolet},
            {"DarkTurquoise", Color.DarkTurquoise},
            {"DarkSlateGray", Color.DarkSlateGray},
            {"DarkSlateBlue", Color.DarkSlateBlue},
            {"DarkSeaGreen", Color.DarkSeaGreen},
            {"DarkSalmon", Color.DarkSalmon},
            {"DarkRed", Color.DarkRed},
            {"DarkOrchid", Color.DarkOrchid},
            {"DarkOrange", Color.DarkOrange},
            {"Firebrick", Color.Firebrick}
        };

        /// <summary>
        /// カラー名称をColorに変換する
        /// </summary>
        /// <param name="name">カラー名称</param>
        /// <returns>Color</returns>
        internal static Color GetColor(string name)
        {
            if (_color.ContainsKey(name))
            {
                return _color[name];
            }

            return Color.SkyBlue;
        }

        /// <summary>
        /// セーブデータのパスを取得する。末尾に\はない。
        /// </summary>
        /// <returns>セーブデータのパス</returns>
        internal static string GetSaveFilePath()
        {
            // パスを取得
            System.Diagnostics.FileVersionInfo info = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), info.CompanyName, info.ProductName, info.FileVersion);

            // パスのフォルダを作成
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
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
        internal static Vector2 GetMsgSize(ref SpriteFont font, string msg, float scale)
        {
            return font.MeasureString(msg) * scale;
        }
    }
}