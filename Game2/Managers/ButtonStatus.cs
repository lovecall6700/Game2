namespace Game2.Managers
{
    /// <summary>
    /// ボタンの状態
    /// </summary>
    internal enum ButtonStatus
    {
        /// <summary>
        /// 解放
        /// </summary>
        Release = 0,
        /// <summary>
        /// クリック(押して離す)
        /// </summary>
        Click,
        /// <summary>
        /// 押下
        /// </summary>
        Press,
        /// <summary>
        /// 連打
        /// </summary>
        Repeat
    }
}
