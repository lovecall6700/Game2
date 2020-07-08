﻿namespace Game2.GameObjects
{
    /// <summary>
    /// ハシゴ
    /// </summary>
    internal class Ladder : GameObject
    {
        internal Ladder(Game2 game2, float x, float y) : base(game2, x, y)
        {
            ObjectKind = GameObjectKind.Ladder;
            Img = Game2.Textures.GetTexture("Images/Ladder");
        }
    }
}