using Game2.GameObjects;
using Game2.Utilities;
using System.Collections.Generic;
using System.Diagnostics;
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
        /// 時間測定
        /// </summary>
        private readonly Stopwatch _stopwatch = new Stopwatch();

        /// <summary>
        /// 時間測定が有効か
        /// </summary>
        internal bool EnableTime = true;

        /// <summary>
        /// Session
        /// </summary>
        public Session()
        {
            LoadHighScore();
        }

        /// <summary>
        /// ストレージからハイスコアをロードする
        /// </summary>
        private void LoadHighScore()
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream fs = new FileStream(Path.Combine(Utility.GetSaveFilePath(), "highscore.dat"), FileMode.Open);
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
                FileStream fs = new FileStream(Path.Combine(Utility.GetSaveFilePath(), "highscore.dat"), FileMode.Create);
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
                FileStream fs = new FileStream(Path.Combine(Utility.GetSaveFilePath(), "stage.dat"), FileMode.Open);
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
                FileStream fs = new FileStream(Path.Combine(Utility.GetSaveFilePath(), "stage.dat"), FileMode.Create);
                formatter.Serialize(fs, data);
                fs.Close();
            }
            catch
            {
            }
        }

        /// <summary>
        /// ハイスコアを更新する
        /// </summary>
        /// <param name="value">スコア</param>
        internal void UpdateHighScore(int value)
        {
            if (value > HighScore)
            {
                HighScore = value;
            }
        }

        /// <summary>
        /// 宝箱の状態を保存する
        /// </summary>
        /// <param name="tb">TreasureBox</param>
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

        /// <summary>
        /// ドアの状態を保存する
        /// </summary>
        /// <param name="d">Door</param>
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

        /// <summary>
        /// アイテムの状態を保存する
        /// </summary>
        /// <param name="d">Item</param>
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


        /// <summary>
        /// 時間測定を開始する
        /// </summary>
        internal void StartTime()
        {
            _stopwatch.Start();
        }

        /// <summary>
        /// 時間測定を終了する
        /// </summary>
        internal void EndTime()
        {
            _stopwatch.Stop();
        }

        /// <summary>
        /// クリア時間を得る
        /// </summary>
        /// <returns>クリア時間</returns>
        internal long CalcTime()
        {
            return _stopwatch.ElapsedMilliseconds;
        }
    }
}