namespace Game2.GameObjects
{
    /// <summary>
    /// 氷ブロック
    /// </summary>
    internal class Ice : Block
    {
        internal Ice(ref Game2 game2, float x, float y) : base(ref game2, x, y)
        {
            Img = Game2.Textures.GetTexture("Ice");
        }

        internal override float GetFriction(float velocity, float controlDirection)
        {
            if (Game2.Inventory.HasShoesItem())
            {
                return base.GetFriction(velocity, controlDirection);
            }

            return -controlDirection * 1f;
        }
    }
}
