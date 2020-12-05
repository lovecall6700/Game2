namespace Game2.GameObjects
{
    internal class EnemyUPeopleBasic : EnemyDog
    {
        internal EnemyUPeopleBasic(Game2 game2, float x, float y) : base(game2, x, y)
        {
            RImg.ClearAndAddImage(Game2.Textures.GetTexture("EnemyUPeopleR1"));
            RImg.AddImage(Game2.Textures.GetTexture("EnemyUPeopleR2"));
            LImg.ClearAndAddImage(Game2.Textures.GetTexture("EnemyUPeopleL1"));
            LImg.AddImage(Game2.Textures.GetTexture("EnemyUPeopleL2"));
            DeadImg = Game2.Textures.GetTexture("EnemyUPeopleDead");
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