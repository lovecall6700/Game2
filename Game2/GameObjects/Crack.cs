using Game2.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.GameObjects
{
    /// <summary>
    /// ひび割れブロック
    /// </summary>
    public class Crack : Block
    {
        private int _life = 5;
        private readonly ImageList _crackImg = new ImageList();
        private readonly Timer _timer = new Timer();
        private readonly int _time = 4;

        public Crack(Game2 game2, float x, float y, string dummy) : base(game2, x, y)
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

        public override void Update(GameTime gameTime)
        {
            if (ObjectKind == GameObjectKinds.Disable)
            {
                return;
            }

            if (!_timer.Update() && Connection.Count != 0)
            {
                _timer.Start(_time);
                _life--;
                Game2.MusicPlayer.PlaySE("SoundEffects/Crack");

                if (_life <= 0)
                {
                    ObjectKind = GameObjectKinds.Disable;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            if (_life > 0)
            {
                spriteBatch.Draw(Game2.Images, Position, _crackImg.GetImage(_life - 1), Color.White);
            }
        }

        public override void Outside()
        {
            //画面外に出たら崩れたブロックは復活する
            if (ObjectKind == GameObjectKinds.Disable)
            {
                Restart();
            }
        }

        public override void Restart()
        {
            _life = 5;
            ObjectKind = GameObjectKinds.Carck;
        }
    }
}