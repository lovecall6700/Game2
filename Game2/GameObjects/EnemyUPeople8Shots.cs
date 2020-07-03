using Game2.Utilities;
using Microsoft.Xna.Framework;

namespace Game2.GameObjects
{
    internal class EnemyUPeople8Shots : EnemyJumpDog
    {
        /// <summary>
        /// 連射間隔
        /// </summary>
        private readonly Timer _bulletTimer = new Timer();

        internal EnemyUPeople8Shots(Game2 game2, float x, float y) : base(game2, x, y)
        {
            RImg[0] = Game2.Textures.GetTexture("Images/EnemyUPeopleR1");
            RImg[1] = Game2.Textures.GetTexture("Images/EnemyUPeopleR2");
            LImg[0] = Game2.Textures.GetTexture("Images/EnemyUPeopleL1");
            LImg[1] = Game2.Textures.GetTexture("Images/EnemyUPeopleL2");
            DeadImg = Game2.Textures.GetTexture("Images/EnemyUPeopleDead");
            Life = 2;
            _bulletTimer.Start(500f, true);
            SetSize(16, 32);
        }

        internal override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);

            if (ObjectStatus != PhysicsObjectStatus.Normal)
            {
                return;
            }

            _bulletTimer.Update(ref gameTime);

            if (!_bulletTimer.Running)
            {
                for (int dir = 0; dir < 360; dir += 45)
                {
                    Game2.PlaySc.PhysicsObjs.Add(new EnemyBullet(Game2, Position.X, Position.Y + 2, dir));
                }

                Game2.MusicPlayer.PlaySE("SoundEffects/EnemyShot");
                _bulletTimer.Start(5000f, true);
            }
        }
    }
}