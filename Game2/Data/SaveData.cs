using Game2.GameObjects;
using System;
using System.Collections.Generic;

namespace Game2
{
    /// <summary>
    /// セーブデータ
    /// </summary>
    [Serializable]
    internal struct SaveData
    {
        /// <summary>
        /// ステージ番号
        /// </summary>
        internal int StageNo;

        /// <summary>
        /// ドア番号
        /// </summary>
        internal int DoorNo;

        /// <summary>
        /// 宝箱の状態
        /// </summary>
        internal Dictionary<string, ObjectVisibility> TreasureBoxVisibility;

        /// <summary>
        /// ドアの状態
        /// </summary>
        internal Dictionary<string, ObjectVisibility> DoorVisibility;

        /// <summary>
        /// アイテムの状態
        /// </summary>
        internal Dictionary<string, ObjectVisibility> ItemVisibility;
    }
}
