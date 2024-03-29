using Game2.Utilities;

namespace Game2.GameObjects
{
    public class EnemyUPeople8Shots : EnemyJumpDog
    {
        /// <summary>
        /// 連射間隔
        /// </summary>
        private readonly Timer _bulletTimer = new Timer();

        public EnemyUPeople8Shots(Game2 game2, float x, float y) : base(game2, x, y)
        {
            RImg.ClearAndAddImage(Game2.Textures.GetTexture("EnemyUPeopleR1"));
            RImg.AddImage(Game2.Textures.GetTexture("EnemyUPeopleR2"));
            LImg.ClearAndAddImage(Game2.Textures.GetTexture("EnemyUPeopleL1"));
            LImg.AddImage(Game2.Textures.GetTexture("EnemyUPeopleL2"));
            DeadImg = Game2.Textures.GetTexture("EnemyUPeopleDead");
            Life = 2;
            _bulletTimer.Start(15);
            SetSize(16, 32);
        }

        public override void Shot()
        {
            for (int dir = 0; dir < 360; dir += 45)
            {
                Game2.PlaySc.PhysicsObjs.Add(new EnemyBullet(Game2, Position.X, Position.Y + 2, dir));
            }

            Game2.MusicPlayer.PlaySE("SoundEffects/EnemyShot");
        }
    }
}