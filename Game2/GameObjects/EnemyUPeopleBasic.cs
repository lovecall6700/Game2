namespace Game2.GameObjects
{
    internal class EnemyUPeopleBasic : EnemyDog
    {
        internal EnemyUPeopleBasic(Game2 game2, float x, float y) : base(game2, x, y)
        {
            RImg[0] = Game2.Textures.GetTexture("Images/EnemyUPeopleR1");
            RImg[1] = Game2.Textures.GetTexture("Images/EnemyUPeopleR2");
            LImg[0] = Game2.Textures.GetTexture("Images/EnemyUPeopleL1");
            LImg[1] = Game2.Textures.GetTexture("Images/EnemyUPeopleL2");
            DeadImg = Game2.Textures.GetTexture("Images/EnemyUPeopleDead");
            Life = 2;
            SetSize(16, 32);
        }

        internal override void Shot()
        {
            Player p = Game2.PlaySc.Player;
            float dir;

            if (ControlDirectionX == 1 && Position.X < p.Position.X)
            {
                dir = 90f;
            }
            else if (ControlDirectionX == -1 && Position.X > p.Position.X)
            {
                dir = 270f;
            }
            else
            {
                return;
            }

            Game2.PlaySc.PhysicsObjs.Add(new EnemyBullet(Game2, Position.X, Position.Y + 2, dir));
            Game2.MusicPlayer.PlaySE("SoundEffects/EnemyShot");
        }
    }
}