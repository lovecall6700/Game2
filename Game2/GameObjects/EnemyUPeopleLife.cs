namespace Game2.GameObjects
{
    public class EnemyUPeopleLife : EnemyUPeopleBasic
    {
        public EnemyUPeopleLife(Game2 game2, float x, float y) : base(game2, x, y)
        {
            Life = 8;
        }
    }
}