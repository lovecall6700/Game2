using Game2.GameObjects;
using Game2.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Game2
{
    /// <summary>
    /// 画面間で共有される情報
    /// </summary>
    internal class Session
    {
        /// <summary>
        /// 宝箱の状態
        /// </summary>
        internal Dictionary<string, ObjectVisibility> TreasureBoxVisibility = new Dictionary<string, ObjectVisibility>();

        /// <summary>
        /// 扉の状態
        /// </summary>
        internal Dictionary<string, ObjectVisibility> DoorVisibility = new Dictionary<string, ObjectVisibility>();

        /// <summary>
        /// アイテムの状態
        /// </summary>
        internal Dictionary<string, ObjectVisibility> ItemVisibility = new Dictionary<string, ObjectVisibility>();

        /// <summary>
        /// ステージ番号
        /// </summary>
        internal int StageNo;

        /// <summary>
        /// ドア番号
        /// </summary>
        internal int DoorNo;

        /// <summary>
        /// 次ステージ番号
        /// </summary>
        internal int DestStageNo;

        /// <summary>
        /// 次ドア番号
        /// </summary>
        internal int DestDoorNo;

        /// <summary>
        /// 制限時間
        /// </summary>
        internal float TimeLimit;

        /// <summary>
        /// ハイスコア
        /// </summary>
        public int HighScore = 0;

        /// <summary>
        /// ライフ
        /// </summary>
        public int Life = Player.MaxLife;

        /// <summary>
        /// 裏技アイテム無限フラグ
        /// </summary>
        public bool InfiniteItem = false;

        /// <summary>
        /// ストレージからハイスコアをロードする
        /// </summary>
        internal void LoadHighScore()
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream fs = new FileStream($@"{Utility.GetSaveFilePath()}\highscore.dat", FileMode.Open);
                HighScoreData sd = (HighScoreData)formatter.Deserialize(fs);
                fs.Close();
                HighScore = sd.HighScore;
                return;
            }
            catch
            {
            }

            HighScore = 0;
        }

        /// <summary>
        /// ストレージにハイスコアをセーブする。
        /// </summary>
        internal void SaveHighScore()
        {
            try
            {
                HighScoreData data = new HighScoreData
                {
                    HighScore = HighScore
                };

                BinaryFormatter formatter = new BinaryFormatter();
                FileStream fs = new FileStream($@"{Utility.GetSaveFilePath()}\highscore.dat", FileMode.Create);
                formatter.Serialize(fs, data);
                fs.Close();
            }
            catch
            {
            }
        }

        /// <summary>
        /// ステージデータをロードする
        /// </summary>
        internal void LoadStage()
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream fs = new FileStream($@"{Utility.GetSaveFilePath()}\stage.dat", FileMode.Open);
                SaveData sd = (SaveData)formatter.Deserialize(fs);
                fs.Close();
                StageNo = sd.StageNo;
                DoorNo = sd.DoorNo;
                TreasureBoxVisibility = sd.TreasureBoxVisibility;
                DoorVisibility = sd.DoorVisibility;
                ItemVisibility = sd.ItemVisibility;
                return;
            }
            catch
            {
            }

            StageNo = Game2.StartStageNo;
            DoorNo = Game2.StartDoorNo;
            TreasureBoxVisibility.Clear();
            DoorVisibility.Clear();
            ItemVisibility.Clear();
        }

        /// <summary>
        /// ステージデータをセーブする。
        /// </summary>
        internal void SaveStage()
        {
            try
            {
                SaveData data = new SaveData
                {
                    StageNo = StageNo,
                    DoorNo = DoorNo,
                    TreasureBoxVisibility = TreasureBoxVisibility,
                    DoorVisibility = DoorVisibility,
                    ItemVisibility = ItemVisibility
                };

                BinaryFormatter formatter = new BinaryFormatter();
                FileStream fs = new FileStream($@"{Utility.GetSaveFilePath()}\stage.dat", FileMode.Create);
                formatter.Serialize(fs, data);
                fs.Close();
            }
            catch
            {
            }
        }

        internal void UpdateHighScore(int value)
        {
            if (value > HighScore)
            {
                HighScore = value;
                SaveHighScore();
            }
        }

        internal void AddTreasureBox(TreasureBox tb)
        {
            string id = tb.GetTreasureBoxID();

            if (TreasureBoxVisibility.ContainsKey(id))
            {
                TreasureBoxVisibility[id] = tb.Visibility;
            }
            else
            {
                TreasureBoxVisibility.Add(id, tb.Visibility);
            }
        }

        internal void AddDoor(Door d)
        {
            string id = d.GetDoorID();

            if (DoorVisibility.ContainsKey(id))
            {
                DoorVisibility[id] = d.Visibility;
            }
            else
            {
                DoorVisibility.Add(id, d.Visibility);
            }
        }

        internal void AddItem(Item d)
        {
            string id = d.GetItemID();

            if (ItemVisibility.ContainsKey(id))
            {
                ItemVisibility[id] = d.Visibility;
            }
            else
            {
                ItemVisibility.Add(id, d.Visibility);
            }
        }
    }
}