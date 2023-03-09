using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Game2.Utilities
{
    public class ImageList
    {
        private int _index = 0;
        private readonly List<Rectangle?> _images = new List<Rectangle?>();

        public void IncIndex()
        {
            if (_images.Count < 2)
            {
                return;
            }

            _index = (_index + 1) % _images.Count;
        }

        public void AddImage(Rectangle? image)
        {
            _images.Add(image);
        }

        public void ClearAndAddImage(Rectangle? image)
        {
            _index = 0;
            _images.Clear();
            _images.Add(image);
        }

        public void ClearImages()
        {
            _index = 0;
            _images.Clear();
        }

        public void ResetIndex()
        {
            _index = 0;
        }

        public Rectangle? GetImage(bool incIndex)
        {
            if (incIndex)
            {
                IncIndex();
            }

            return GetImage(_index);
        }

        public Rectangle? GetImage(int index)
        {
            return _images.Count == 0 || index < 0 || _images.Count <= index ? null : _images[index];
        }
    }
}
