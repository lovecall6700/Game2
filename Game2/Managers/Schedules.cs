namespace Game2.Managers
{
    /// <summary>
    /// 画面の切り替え・状態一覧
    /// </summary>
    internal enum Schedules : int
    {
        None = 1,
        RestartOrGameover,
        EnterDoor,
        Ending,
        GameStart,
        Title,
        Retry,
        Quit,
        InitialStart,
        ContinueStart,
        SaveStage,
        BGMVolume,
        SEVolume,
        Options,
        Story
    }
}
