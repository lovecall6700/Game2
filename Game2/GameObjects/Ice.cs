namespace Game2.GameObjects
{
    /// <summary>
    /// 氷ブロック
    /// </summary>
    public class Ice : Block
    {
        public Ice(Game2 game2, float x, float y) : base(game2, x, y)
        {
            Img = Game2.Textures.GetTexture("Ice");
        }

        public override float GetFriction(float velocity, float controlDirection)
        {
            if (Game2.Inventory.HasShoesItem())
            {
                return base.GetFriction(velocity, controlDirection);
            }

            return -controlDirection * 1f;
        }
    }
}
