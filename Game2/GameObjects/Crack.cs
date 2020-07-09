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
        private readonly Texture2D[] _crackImg = new Texture2D[5];
        private readonly Timer _timer = new Timer();
        private readonly float _time = 200f;

        internal Crack(Game2 game2, float x, float y, string dummy) : base(game2, x, y)
        {
            ObjectKind = GameObjectKind.Carck;
            Img = Game2.Textures.GetTexture("Images/" + dummy);
            _crackImg[0] = Game2.Textures.GetTexture("Images/Crack3");
            _crackImg[1] = Game2.Textures.GetTexture("Images/Crack2");
            _crackImg[2] = Game2.Textures.GetTexture("Images/Crack1");
            _crackImg[3] = Game2.Textures.GetTexture("Images/Null");
            _crackImg[4] = Game2.Textures.GetTexture("Images/Null");
            SetSize(16, 16);
        }

        internal override void Update(ref GameTime gameTime)
        {
            if (ObjectKind == GameObjectKind.Disable)
            {
                return;
            }

            _timer.Update(ref gameTime);

            if (Connection.Count != 0 && !_timer.Running)
            {
                _timer.Start(_time, true);
                _life--;
                Game2.MusicPlayer.PlaySE("SoundEffects/Crack");

                if (_life <= 0)
                {
                    ObjectKind = GameObjectKind.Disable;
                }
            }

            base.Update(ref gameTime);
        }

        internal override void Draw(ref Vector2 offset, ref GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            base.Draw(ref offset, ref gameTime, ref spriteBatch);

            if (_life > 0)
            {
                spriteBatch.Draw(_crackImg[_life - 1], Position - offset, Color.White);
            }
        }

        internal override void Outside()
        {
            //画面外に出たら崩れたブロックは復活する
            if (ObjectKind == GameObjectKind.Disable)
            {
                Restart();
            }
        }

        internal override void Restart()
        {
            _life = 5;
            ObjectKind = GameObjectKind.Carck;
        }
    }
}