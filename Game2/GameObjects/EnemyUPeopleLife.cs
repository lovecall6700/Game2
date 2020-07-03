namespace Game2.GameObjects
{
    internal class EnemyUPeopleLife : EnemyUPeopleBasic
    {
        internal EnemyUPeopleLife(Game2 game2, float x, float y) : base(game2, x, y)
        {
            Life = 8;
        }
    }
}