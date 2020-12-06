using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        /// 拡大率
        /// </summary>
        internal float Zoom;

        /// <summary>
        /// レンダーターゲットの矩形
        /// </summary>
        internal Viewport Viewport;

        internal void Initialize(GraphicsDevice device, int width, int height)
        {
            int backWidth = device.PresentationParameters.BackBufferWidth;
            int backHeight = device.PresentationParameters.BackBufferHeight;

            if (backWidth > backHeight)
            {
                //横長
                Zoom = (float)backHeight / height;
                Viewport = new Viewport((backWidth - backHeight) / 2, 0, backHeight, backHeight);
            }
            else if (backWidth < backHeight)
            {
                //縦長
                Zoom = (float)backWidth / width;
                Viewport = new Viewport(0, (backHeight - backWidth) / 2, backWidth, backWidth);
            }
            else
            {
                //正方形
                Zoom = (float)backHeight / height;
                Viewport = new Viewport(0, 0, backHeight, backHeight);
            }

            Transform = Matrix.CreateScale(new Vector3(Zoom, Zoom, 1));
            device.Viewport = Viewport;
        }
    }
}
