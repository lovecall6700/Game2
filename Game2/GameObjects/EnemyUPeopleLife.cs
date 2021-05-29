namespace Game2.GameObjects
{
    internal class EnemyUPeopleLife : EnemyUPeopleBasic
    {
        internal EnemyUPeopleLife(ref Game2 game2, float x, float y) : base(ref game2, x, y)
        {
            Life = 8;
        }
    }
}