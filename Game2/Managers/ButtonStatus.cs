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
        /// クリック判定後に内部処理用としてクリック状態を保存
        /// </summary>
        Click2,
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
