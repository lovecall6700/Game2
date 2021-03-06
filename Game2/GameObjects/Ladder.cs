namespace Game2.GameObjects
{
    /// <summary>
    /// ハシゴ
    /// </summary>
    internal class Ladder : GameObject
    {
        internal Ladder(ref Game2 game2, float x, float y) : base(ref game2, x, y)
        {
            ObjectKind = GameObjectKinds.Ladder;
            Img = Game2.Textures.GetTexture("Ladder");
        }
    }
}
