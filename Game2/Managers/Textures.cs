using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game2.Managers
{
    /// <summary>
    /// テクスチャ管理
    /// </summary>
    internal class Textures
    {
        private readonly ContentManager _content;

        /// <summary>
        /// テクスチャ名でテクスチャを管理
        /// </summary>
        private readonly Dictionary<string, Texture2D> Images = new Dictionary<string, Texture2D>();

        public Textures(ContentManager content)
        {
            _content = content;
        }

        /// <summary>
        /// テクスチャを取得する
        /// </summary>
        /// <param name="name">テクスチャ名</param>
        /// <returns>テクスチャ</returns>
        internal Texture2D GetTexture(string name)
        {
            if (Images.ContainsKey(name))
            {
                return Images[name];
            }

            Texture2D t = null;

            try
            {
                t = _content.Load<Texture2D>(name);
                Images.Add(name, t);
            }
            catch
            {

            }

            return t;
        }

        /// <summary>
        /// テクスチャを破棄する
        /// </summary>
        /// <param name="name">テクスチャ名</param>
        internal void UnLoad(string name)
        {
            Texture2D t = Images[name];
            t.Dispose();
            Images.Remove(name);
        }
    }
}
