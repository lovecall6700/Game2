namespace Game2.GameObjects
{
    /// <summary>
    /// 雲
    /// </summary>
    internal class Cloud : GameObject
    {
        /// <summary>
        /// 雲
        /// </summary>
        /// <param name="game2">Game2</param>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        internal Cloud(Game2 game2, float x, float y) : base(game2, x, y)
        {
            ObjectKind = GameObjectKind.Cloud;
            Img = Game2.Textures.GetTexture("Images/Cloud");
            SetSize(32, 16);
        }
    }
}
