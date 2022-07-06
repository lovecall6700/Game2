namespace Game2.GameObjects
{
    /// <summary>
    /// 雲
    /// </summary>
    public class Cloud : GameObject
    {
        /// <summary>
        /// 雲
        /// </summary>
        /// <param name="game2">Game2</param>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        public Cloud(Game2 game2, float x, float y) : base(game2, x, y)
        {
            ObjectKind = GameObjectKinds.Cloud;
            Img = Game2.Textures.GetTexture("Cloud");
            SetSize(32, 16);
        }
    }
}
