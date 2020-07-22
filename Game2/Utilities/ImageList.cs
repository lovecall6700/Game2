using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game2.Utilities
{
    internal class ImageList
    {
        private int _index = 0;
        private readonly List<Texture2D> _images = new List<Texture2D>();

        internal void IncIndex()
        {
            if (_images.Count < 2)
            {
                return;
            }

            _index = (_index + 1) % _images.Count;
        }

        internal void AddImage(Texture2D image)
        {
            _images.Add(image);
        }

        internal void ClearImages()
        {
            _images.Clear();
        }

        internal void ResetIndex()
        {
            _index = 0;
        }

        internal Texture2D GetImage(bool incIndex)
        {
            if (incIndex)
            {
                IncIndex();
            }

            return GetImage(_index);
        }

        internal Texture2D GetImage(int index)
        {
            if (_images.Count == 0 || index < 0 || _images.Count <= index)
            {
                return null;
            }

            return _images[_index];
        }
    }
}
