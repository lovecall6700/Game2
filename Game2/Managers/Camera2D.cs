using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game2.Managers
{
    /// <summary>
    /// 2Dカメラ
    /// </summary>
    internal class Camera2D
    {
        /// <summary>
        /// 画面サイズに合わせた画面引き延ばしの行列
        /// </summary>
        internal Matrix Transform;

        /// <summary>
        /// 拡大・縮小行列
        /// </summary>
        private Matrix _scale;

        /// <summary>
        /// レンダーターゲットの矩形
        /// </summary>
        private Viewport _viewport;

        /// <summary>
        /// カメラ位置
        /// </summary>
        public Vector2 Position = Vector2.Zero;

        internal void Initialize(GraphicsDevice device, int width, int height)
        {
            int backWidth = device.PresentationParameters.BackBufferWidth;
            int backHeight = device.PresentationParameters.BackBufferHeight;
            float scale;

            if (backWidth > backHeight)
            {
                //横長
                scale = (float)backHeight / height;
                _viewport = new Viewport((backWidth - backHeight) / 2, 0, backHeight, backHeight);
            }
            else if (backWidth < backHeight)
            {
                //縦長
                scale = (float)backWidth / width;
                _viewport = new Viewport(0, (backHeight - backWidth) / 2, backWidth, backWidth);
            }
            else
            {
                //正方形
                scale = (float)backHeight / height;
                _viewport = new Viewport(0, 0, backHeight, backHeight);
            }

            Position = Vector2.Zero;
            scale = (float)Math.Round(scale, 2);
            _scale = Matrix.CreateScale(new Vector3(scale, scale, 1));
            CalcTransform();
            device.Viewport = _viewport;
        }

        /// <summary>
        /// カメラの行列を計算する
        /// </summary>
        public void CalcTransform()
        {
            Matrix translationMatrix = Matrix.CreateTranslation(new Vector3(Position.X, Position.Y, 0));
            Transform = Matrix.Invert(translationMatrix) * _scale;
        }

        internal void Focus(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
            CalcTransform();
        }

        internal void FocusX(int x)
        {
            Position.X = x;
            CalcTransform();
        }

        internal void FocusY(int y)
        {
            Position.Y = y;
            CalcTransform();
        }
    }
}
