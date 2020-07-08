namespace Game2.GameObjects
{
    /// <summary>
    /// ゲームオブジェクト種別一覧
    /// </summary>
    internal enum GameObjectKind : int
    {
        Player = 1,
        Enemy,
        Block,
        PlayerBullet,
        Door,
        Cloud,
        TreasureBox,
        ZeroFrictionBlock,
        Item,
        Ladder,
        MovingFloor
    }
}
