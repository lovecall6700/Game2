using Game2.GameObjects;
using Game2.Managers;
using Game2.Utilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Game2
{
    /// <summary>
    /// 画面間で共有される情報
    /// </summary>
    public class Session
    {
        /// <summary>
        /// 宝箱の状態
        /// </summary>
        public Dictionary<string, ObjectVisibility> TreasureBoxVisibility = new Dictionary<string, ObjectVisibility>();

        /// <summary>
        /// 扉の状態
        /// </summary>
        public Dictionary<string, ObjectVisibility> DoorVisibility = new Dictionary<string, ObjectVisibility>();

        /// <summary>
        /// アイテムの状態
        /// </summary>
        public Dictionary<string, ObjectVisibility> ItemVisibility = new Dictionary<string, ObjectVisibility>();

        /// <summary>
        /// ステージ番号
        /// </summary>
        public int StageNo;

        /// <summary>
        /// ドア番号
        /// </summary>
        public int DoorNo;

        /// <summary>
        /// 次ステージ番号
        /// </summary>
        public int DestStageNo;

        /// <summary>
        /// 次ドア番号
        /// </summary>
        public int DestDoorNo;

        /// <summary>
        /// 制限時間
        /// </summary>
        public int TimeLimit;

        /// <summary>
        /// ハイスコア
        /// </summary>
        public int HighScore = 0;

        /// <summary>
        /// スコア
        /// </summary>
        public int Score = 0;

        /// <summary>
        /// 残機
        /// </summary>
        public int Remain = 2;

        /// <summary>
        /// エクステンド回数
        /// </summary>
        private int _extendCount = 0;

        /// <summary>
        /// 前回エクステンドスコア
        /// </summary>
        private int _lastExtendScore;

        /// <summary>
        /// エクステンドスコア
        /// </summary>
        private readonly int[] _extendScores = new[] { 10000, 20000, 30000, 50000 };

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
        public bool EnableTime = true;

        /// <summary>
        /// ゲーム開始時のステージ番号
        /// </summary>
        public static readonly int StartStageNo = 1;

        /// <summary>
        /// ゲーム開始時のドア番号
        /// </summary>
        public static readonly int StartDoorNo = 3;

        /// <summary>
        /// アイテム管理
        /// </summary>
        public Inventory Inventory = new Inventory();

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
            FileStream fs = null;

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                fs = new FileStream(Path.Combine(Utility.GetSaveFilePath(), "highscore.dat"), FileMode.Open);
                HighScoreData sd = (HighScoreData)formatter.Deserialize(fs);
                HighScore = sd.HighScore;
            }
            catch
            {
                HighScore = 0;
            }
            finally
            {
                fs?.Close();
            }
        }

        /// <summary>
        /// ストレージにハイスコアをセーブする。
        /// </summary>
        public void SaveHighScore()
        {
            FileStream fs = null;

            try
            {
                HighScoreData data = new HighScoreData
                {
                    HighScore = HighScore
                };

                BinaryFormatter formatter = new BinaryFormatter();
                fs = new FileStream(Path.Combine(Utility.GetSaveFilePath(), "highscore.dat"), FileMode.Create);
                formatter.Serialize(fs, data);
            }
            catch
            {
            }
            finally
            {
                fs?.Close();
            }
        }

        /// <summary>
        /// ステージデータをロードする
        /// </summary>
        public void LoadStage()
        {
            FileStream fs = null;

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                fs = new FileStream(Path.Combine(Utility.GetSaveFilePath(), "stage.dat"), FileMode.Open);
                SaveData sd = (SaveData)formatter.Deserialize(fs);
                StageNo = sd.StageNo;
                DoorNo = sd.DoorNo;
                TreasureBoxVisibility = sd.TreasureBoxVisibility;
                DoorVisibility = sd.DoorVisibility;
                ItemVisibility = sd.ItemVisibility;
            }
            catch
            {
                StageNo = StartStageNo;
                DoorNo = StartDoorNo;
                TreasureBoxVisibility.Clear();
                DoorVisibility.Clear();
                ItemVisibility.Clear();
            }
            finally
            {
                fs?.Close();
            }
        }

        /// <summary>
        /// ステージデータをセーブする。
        /// </summary>
        public void SaveStage()
        {
            FileStream fs = null;

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
                fs = new FileStream(Path.Combine(Utility.GetSaveFilePath(), "stage.dat"), FileMode.Create);
                formatter.Serialize(fs, data);
            }
            catch
            {
            }
            finally
            {
                fs?.Close();
            }
        }

        /// <summary>
        /// 宝箱の状態を保存する
        /// </summary>
        /// <param name="tb">TreasureBox</param>
        public void AddTreasureBox(TreasureBox tb)
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
        public void AddDoor(Door d)
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
        public void AddItem(Item d)
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
        public void StartTime()
        {
            _stopwatch.Start();
        }

        /// <summary>
        /// 時間測定を終了する
        /// </summary>
        public void EndTime()
        {
            _stopwatch.Stop();
        }

        /// <summary>
        /// クリア時間を得る
        /// </summary>
        /// <returns>クリア時間</returns>
        public long CalcTime()
        {
            return _stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// スコアを加算する
        /// スコアがハイスコアを上回った場合は更新する
        /// </summary>
        /// <param name="score">スコア</param>
        public void AddScore(int score, bool doubleScoreItem)
        {
            int s = score * (doubleScoreItem ? 2 : 1);
            Score += s;
            _lastExtendScore += s;

            if (Score > HighScore)
            {
                HighScore = Score;
            }

            while (true)
            {
                if (_extendScores[_extendCount] <= _lastExtendScore)
                {
                    _lastExtendScore -= _extendScores[_extendCount];
                    _extendCount = MathHelper.Clamp(_extendCount + 1, 0, _extendScores.Length - 1);
                    Remain = MathHelper.Clamp(Remain + 1, 0, 10);
                    continue;
                }

                break;
            }
        }

        /// <summary>
        /// ミスが発生
        /// </summary>
        /// <returns>ゲームオーバーか</returns>
        public bool Miss()
        {
            Remain--;
            return Remain < 0;
        }

        public void GameoverRetryToStart()
        {
            Score = 0;
            Remain = 2;
            _lastExtendScore = 0;
            _extendCount = 0;
            Inventory.SetAllFlags(InfiniteItem);
        }
    }
}