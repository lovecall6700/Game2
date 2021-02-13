namespace Game2.GameObjects
{
    /// <summary>
    /// 可視性一覧
    /// </summary>
    internal enum ObjectVisibility : int
    {
        /// <summary>
        /// 通常
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 不可視、しゃがむことで発見
        /// </summary>
        Hidden,
        /// <summary>
        /// 不可視だが効果は有効
        /// </summary>
        Invisible,
        /// <summary>
        /// 不可視かつ効果も無効
        /// </summary>
        Disable,
        /// <summary>
        /// 開いている
        /// </summary>
        Open
    }
}