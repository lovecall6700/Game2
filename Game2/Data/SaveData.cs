using Game2.GameObjects;
using System;
using System.Collections.Generic;

namespace Game2
{
    /// <summary>
    /// セーブデータ
    /// </summary>
    [Serializable]
    public struct SaveData
    {
        /// <summary>
        /// ステージ番号
        /// </summary>
        public int StageNo;

        /// <summary>
        /// ドア番号
        /// </summary>
        public int DoorNo;

        /// <summary>
        /// 宝箱の状態
        /// </summary>
        public Dictionary<string, ObjectVisibility> TreasureBoxVisibility;

        /// <summary>
        /// ドアの状態
        /// </summary>
        public Dictionary<string, ObjectVisibility> DoorVisibility;

        /// <summary>
        /// アイテムの状態
        /// </summary>
        public Dictionary<string, ObjectVisibility> ItemVisibility;
    }
}
