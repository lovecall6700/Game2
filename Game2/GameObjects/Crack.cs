using Game2.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.GameObjects
{
    /// <summary>
    /// ひび割れブロック
    /// </summary>
    internal class Crack : Block
    {
        private int _life = 5;
        private readonly ImageList _crackImg = new ImageList();
        private readonly Timer _timer = new Timer();
        private readonly float _time = 120f;

        internal Crack(ref Game2 game2, float x, float y, string dummy) : base(ref game2, x, y)
        {
            ObjectKind = GameObjectKinds.Carck;
            Img = Game2.Textures.GetTexture("" + dummy);
            _crackImg.AddImage(Game2.Textures.GetTexture("Crack3"));
            _crackImg.AddImage(Game2.Textures.GetTexture("Crack2"));
            _crackImg.AddImage(Game2.Textures.GetTexture("Crack1"));
            _crackImg.AddImage(Game2.Textures.GetTexture("Null"));
            _crackImg.AddImage(Game2.Textures.GetTexture("Null"));
            SetSize(16, 16);
        }

        internal override void Update(ref GameTime gameTime)
        {
            if (ObjectKind == GameObjectKinds.Disable)
            {
                return;
            }

            if (!_timer.Update(ref gameTime) && Connection.Count != 0)
            {
                _timer.Start(_time, true);
                _life--;
                Game2.MusicPlayer.PlaySE("SoundEffects/Crack");

                if (_life <= 0)
                {
                    ObjectKind = GameObjectKinds.Disable;
                }
            }

            base.Update(ref gameTime);
        }

        internal override void Draw(ref Vector2 offset, ref GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            base.Draw(ref offset, ref gameTime, ref spriteBatch);

            if (_life > 0)
            {
                spriteBatch.Draw(Game2.Images, Position - offset, _crackImg.GetImage(_life - 1), Color.White);
            }
        }

        internal override void Outside()
        {
            //画面外に出たら崩れたブロックは復活する
            if (ObjectKind == GameObjectKinds.Disable)
            {
                Restart();
            }
        }

        internal override void Restart()
        {
            _life = 5;
            ObjectKind = GameObjectKinds.Carck;
        }
    }
}