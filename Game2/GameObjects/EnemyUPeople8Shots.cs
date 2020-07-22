using Game2.Utilities;

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
            RImg.ClearAndAddImage(Game2.Textures.GetTexture("Images/EnemyUPeopleR1"));
            RImg.AddImage(Game2.Textures.GetTexture("Images/EnemyUPeopleR2"));
            LImg.ClearAndAddImage(Game2.Textures.GetTexture("Images/EnemyUPeopleL1"));
            LImg.AddImage(Game2.Textures.GetTexture("Images/EnemyUPeopleL2"));
            DeadImg = Game2.Textures.GetTexture("Images/EnemyUPeopleDead");
            Life = 2;
            _bulletTimer.Start(500f, true);
            SetSize(16, 32);
        }

        internal override void Shot()
        {
            for (int dir = 0; dir < 360; dir += 45)
            {
                Game2.PlaySc.PhysicsObjs.Add(new EnemyBullet(Game2, Position.X, Position.Y + 2, dir));
            }

            Game2.MusicPlayer.PlaySE("SoundEffects/EnemyShot");
        }
    }
}