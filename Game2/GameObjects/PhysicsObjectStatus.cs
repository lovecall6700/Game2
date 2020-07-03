namespace Game2.GameObjects
{
    /// <summary>
    /// オブジェクト状態の一覧
    /// </summary>
    internal enum PhysicsObjectStatus : int
    {
        /// <summary>
        /// 通常
        /// </summary>
        Normal = 1,
        /// <summary>
        /// ダメージ
        /// </summary>
        Damage,
        /// <summary>
        /// 死亡
        /// </summary>
        Dead,
        /// <summary>
        /// 除去
        /// </summary>
        Remove
    }
}
