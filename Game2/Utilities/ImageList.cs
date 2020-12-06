using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Game2.Utilities
{
    internal class ImageList
    {
        private int _index = 0;
        private readonly List<Rectangle?> _images = new List<Rectangle?>();

        internal void IncIndex()
        {
            if (_images.Count < 2)
            {
                return;
            }

            _index = (_index + 1) % _images.Count;
        }

        internal void AddImage(Rectangle? image)
        {
            _images.Add(image);
        }

        internal void ClearAndAddImage(Rectangle? image)
        {
            _index = 0;
            _images.Clear();
            _images.Add(image);
        }

        internal void ClearImages()
        {
            _index = 0;
            _images.Clear();
        }

        internal void ResetIndex()
        {
            _index = 0;
        }

        internal Rectangle? GetImage(bool incIndex)
        {
            if (incIndex)
            {
                IncIndex();
            }

            return GetImage(_index);
        }

        internal Rectangle? GetImage(int index)
        {
            if (_images.Count == 0 || index < 0 || _images.Count <= index)
            {
                return null;
            }

            return _images[index];
        }
    }
}
