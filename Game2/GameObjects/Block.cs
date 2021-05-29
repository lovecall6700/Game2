namespace Game2.GameObjects
{
    /// <summary>
    /// Block
    /// </summary>
    internal class Block : GameObject
    {
        /// <summary>
        /// Block
        /// </summary>
        /// <param name="game2">Game2</param>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        internal Block(ref Game2 game2, float x, float y) : base(ref game2, x, y)
        {
        }
    }
}
