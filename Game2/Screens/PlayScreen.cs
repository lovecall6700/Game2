using Game2.GameObjects;
using Game2.Managers;
using Game2.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace Game2.Screens
{
    /// <summary>
    /// ゲームプレイ画面
    /// </summary>
    public class PlayScreen : Screen
    {
        /// <summary>
        /// 描画範囲より少し大きい範囲
        /// </summary>
        private Rectangle _sight = new Rectangle(0, 0, 288, 288);

        /// <summary>
        /// ステージの構成
        /// </summary>
        public StageDirType StageDir;

        /// <summary>
        /// 非接触の前面オブジェクト
        /// </summary>
        private readonly List<GameObject> _frontObjs = new List<GameObject>();
        private readonly List<GameObject> _nearFrontObjs = new List<GameObject>();

        /// <summary>
        /// 接触オブジェクト
        /// </summary>
        private readonly List<GameObject> _mapObjs = new List<GameObject>();
        public List<GameObject> NearMapObjs = new List<GameObject>();

        /// <summary>
        /// 非接触の背面オブジェクト
        /// </summary>
        private readonly List<GameObject> _backObjs = new List<GameObject>();

        /// <summary>
        /// 非接触の背面オブジェクト(近接)
        /// </summary>
        private readonly List<GameObject> _nearBackObjs = new List<GameObject>();

        /// <summary>
        /// 宝箱・アイテムオブジェクト
        /// </summary>
        public List<GameObject> ItemObjs = new List<GameObject>();

        /// <summary>
        /// 動くオブジェクト
        /// </summary>
        public List<PhysicsObject> PhysicsObjs = new List<PhysicsObject>();

        /// <summary>
        /// エフェクト用オブジェクト
        /// </summary>
        public List<PhysicsObject> EffectObjs = new List<PhysicsObject>();

        /// <summary>
        /// 背景色を切り替えるか
        /// </summary>
        private bool _backColorSwitchable;

        /// <summary>
        /// マップ幅
        /// </summary>
        private int _mapWidth;

        /// <summary>
        /// マップ高さ
        /// </summary>
        private int _mapHeight;

        /// <summary>
        /// Y軸方向下方向、画面外の限界
        /// </summary>
        public float OutOfMapY;

        /// <summary>
        /// 背景色
        /// </summary>
        private readonly Color[] _backColors = new Color[2];

        /// <summary>
        /// 背景色切り替え時間
        /// </summary>
        private readonly int[] _backColorSwitchTimes = new int[2];

        /// <summary>
        /// 切り替えタイマー
        /// </summary>
        private readonly Timer _backColorSwitchTimer = new Timer();

        /// <summary>
        /// 制限時間
        /// </summary>
        private int _timeLimit;

        /// <summary>
        /// 暗闇ステージ
        /// </summary>
        private bool _darkZone;

        /// <summary>
        /// 暗闇ステージの明滅間隔
        /// </summary>
        private readonly int[] _darkZoneSwitchTimes = new int[2];

        /// <summary>
        /// 暗闇ステージの切り替えタイマー
        /// </summary>
        private readonly Timer _darkZoneSwitchTimer = new Timer();

        /// <summary>
        /// 暗闇ステージの明暗状態
        /// </summary>
        private bool _darkZoneSwitch;

        /// <summary>
        /// プレーヤー
        /// </summary>
        public Player Player;

        /// <summary>
        /// BGM名
        /// </summary>
        private string _songName;

        /// <summary>
        /// 敵生成
        /// </summary>
        private EnemyGenerator _enemyGenerator;

        /// <summary>
        /// 隠し扉発見スコア
        /// </summary>
        public static readonly int FindBonus = 1000;

        /// <summary>
        /// ライフ表示
        /// </summary>
        private readonly LifeDisplay _lifeDisp;

        /// <summary>
        /// 残機表示
        /// </summary>
        private readonly RemainDisplay _remainDisp;

        /// <summary>
        /// 制限時間表示
        /// </summary>
        private readonly TimeLimitDisplay _timeLimitDisp;

        /// <summary>
        /// 制限時間タイマー
        /// </summary>
        public Timer Timer = new Timer();

        /// <summary>
        /// 低速時タイマー制御
        /// </summary>
        private bool _slow = false;

        /// <summary>
        /// スコア表示
        /// </summary>
        private readonly ScoreDisplay _scoreDisp;

        public PlayScreen(Game2 game2) : base(game2)
        {
            _lifeDisp = new LifeDisplay(game2);
            _remainDisp = new RemainDisplay(game2);
            _timeLimitDisp = new TimeLimitDisplay(game2);
            _scoreDisp = new ScoreDisplay(game2);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!GetDarkZone() || Game2.Session.Inventory.HasLightItem())
            {
                foreach (GameObject item in _nearBackObjs)
                {
                    item.Draw(spriteBatch);
                }

                foreach (GameObject item in NearMapObjs)
                {
                    item.Draw(spriteBatch);
                }
            }

            foreach (GameObject item in ItemObjs)
            {
                item.Draw(spriteBatch);
            }

            foreach (GameObject item in PhysicsObjs)
            {
                item.Draw(spriteBatch);
            }

            if (!GetDarkZone() || Game2.Session.Inventory.HasLightItem())
            {
                foreach (GameObject item in _nearFrontObjs)
                {
                    item.Draw(spriteBatch);
                }
            }

            foreach (GameObject item in EffectObjs)
            {
                item.Draw(spriteBatch);
            }

            _lifeDisp.Draw(spriteBatch);
            _remainDisp.Draw(spriteBatch);
            _timeLimitDisp.Draw(spriteBatch);
            _scoreDisp.Draw(spriteBatch);
        }

        /// <summary>
        /// 描画する背景色を返す
        /// </summary>
        /// <returns>背景色</returns>
        public override Color GetBackColor()
        {
            if (_backColors[0] == _backColors[1])
            {
                return _backColors[0];
            }

            if (!_backColorSwitchTimer.Running)
            {
                _backColorSwitchable = !_backColorSwitchable;
                _backColorSwitchTimer.Start(_backColorSwitchTimes[_backColorSwitchable ? 0 : 1]);
            }

            return _backColorSwitchable ? _backColors[0] : _backColors[1];
        }

        /// <summary>
        /// 暗闇ステージの明暗状態を返す
        /// </summary>
        /// <returns>暗闇ステージの明暗状態</returns>
        private bool GetDarkZone()
        {
            if (!_darkZone)
            {
                return false;
            }

            if (!_darkZoneSwitchTimer.Running)
            {
                _darkZoneSwitch = !_darkZoneSwitch;
                _darkZoneSwitchTimer.Start(_darkZoneSwitchTimes[_darkZoneSwitch ? 0 : 1]);
            }

            return _darkZoneSwitch;
        }

        /// <summary>
        /// ゲーム再開
        /// </summary>
        public void Restart()
        {
            PhysicsObjs.Clear();
            EffectObjs.Clear();
            NearMapObjs.Clear();
            Player.Restart();
            ResetMap();
            PhysicsObjs.Add(Player);
            _backColorSwitchTimer.Running = false;
            _darkZoneSwitchTimer.Running = false;

            foreach (GameObject item in _mapObjs)
            {
                item.Restart();
            }

            Timer.Start(Game2.Session.TimeLimit);
        }

        /// <summary>
        /// ステージを読み込む
        /// </summary>
        public void LoadStage()
        {
            LoadSettings();
            LoadMap();
        }

        /// <summary>
        /// マップをリセットする
        /// </summary>
        private void ResetMap()
        {
            foreach (GameObject item in _mapObjs)
            {
                //移動床は画面外でも動かす
                if (item.ObjectKind == GameObjectKinds.MovingFloor)
                {
                    MovingFloor mf = (MovingFloor)item;
                    mf.ResetPosition();
                }
            }
        }

        /// <summary>
        /// マップを読み込む
        /// </summary>
        private void LoadMap()
        {
            int stageNo = Game2.Session.StageNo;
            int startDoorNo = Game2.Session.DoorNo;
            _frontObjs.Clear();
            _nearFrontObjs.Clear();
            _mapObjs.Clear();
            NearMapObjs.Clear();
            _backObjs.Clear();
            _nearBackObjs.Clear();
            ItemObjs.Clear();
            PhysicsObjs.Clear();
            EffectObjs.Clear();
            _backColorSwitchable = false;
            int treasureBoxNo = 0;
            int itemNo = 0;

            foreach (string line in File.ReadLines($"Content/Stages/Stage{stageNo}_map.txt"))
            {
                string[] lines = line.Split(',');

                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i] = lines[i].Trim();
                }

                float x = int.Parse(lines[0]) * 16;
                float y = int.Parse(lines[1]) * 16;
                string objectName = lines[2];
                int depth = 0;
                GameObject obj = null;

                switch (objectName)
                {
                    case "Door":

                        int doorNo = int.Parse(lines[3]);

                        //ドアの生成
                        //例: 71,10,Door,1,Normal,1,0,Null,Null                        
                        int destStageNo = int.Parse(lines[5]);
                        int destDoorNo = int.Parse(lines[6]);
                        Door door = new Door(Game2, x, y, stageNo, doorNo, lines[4], destStageNo, destDoorNo, lines[7], lines[8]);

                        if (Game2.Session.DoorVisibility.ContainsKey(door.GetDoorID()))
                        {
                            door.Visibility = Game2.Session.DoorVisibility[door.GetDoorID()];
                        }

                        ItemObjs.Add(door);

                        if (doorNo == startDoorNo)
                        {
                            //主人公スタート位置の決定
                            Player = new Player(Game2, x, y);
                            PhysicsObjs.Add(Player);
                        }

                        break;

                    case "TreasureBox":

                        TreasureBox treasureBox = new TreasureBox(Game2, x, y, bool.Parse(lines[3]), int.Parse(lines[4]), lines[5], stageNo, treasureBoxNo);

                        if (Game2.Session.TreasureBoxVisibility.ContainsKey(treasureBox.GetTreasureBoxID()))
                        {
                            treasureBox.Visibility = Game2.Session.TreasureBoxVisibility[treasureBox.GetTreasureBoxID()];
                        }

                        treasureBoxNo++;
                        ItemObjs.Add(treasureBox);
                        break;

                    case "Item":

                        Item item = new Item(Game2, x, y, stageNo, itemNo, lines[3], bool.Parse(lines[4]), lines[5]);

                        if (Game2.Session.ItemVisibility.ContainsKey(item.GetItemID()))
                        {
                            item.Visibility = Game2.Session.ItemVisibility[item.GetItemID()];
                        }

                        itemNo++;
                        ItemObjs.Add(item);
                        break;

                    case "Cloud":

                        obj = new Cloud(Game2, x, y);
                        depth = int.Parse(lines[3]);
                        break;

                    case "Ice":

                        obj = new Ice(Game2, x, y);
                        depth = int.Parse(lines[3]);
                        break;

                    case "Ladder":

                        obj = new Ladder(Game2, x, y);
                        depth = int.Parse(lines[3]);
                        break;

                    case "MovingFloor":

                        obj = new MovingFloor(Game2, x, y, lines[3], float.Parse(lines[4]), float.Parse(lines[5]));
                        break;

                    case "Crack":

                        obj = new Crack(Game2, x, y, lines[3]);
                        break;

                    case "BeltConveyer":

                        obj = new BeltConveyer(Game2, x, y, lines[3], lines[4]);
                        break;

                    case "StaticMessage":

                        obj = new StaticMessage(Game2, x, y, lines[3]);
                        depth = -1;
                        break;

                    default:

                        //Block系
                        obj = new Block(Game2, x, y)
                        {
                            Img = Game2.Textures.GetTexture($"{objectName}")
                        };

                        depth = int.Parse(lines[3]);
                        break;
                }

                if (obj == null)
                {
                    //Do nothing
                }
                else if (depth < 0)
                {
                    _backObjs.Add(obj);
                }
                else if (depth > 0)
                {
                    _frontObjs.Add(obj);
                }
                else
                {
                    _mapObjs.Add(obj);
                }
            }
        }

        /// <summary>
        /// ステージの設定を読み込む
        /// </summary>
        private void LoadSettings()
        {
            int stageNo = Game2.Session.StageNo;
            string[] settings = File.ReadAllLines($"Content/Stages/Stage{stageNo}_settings.txt");
            int index = 0;
            _mapWidth = int.Parse(settings[index++]);
            _mapHeight = int.Parse(settings[index++]);
            OutOfMapY = (_mapHeight + 2) * 16;

            if (_mapWidth == _mapHeight)
            {
                StageDir = StageDirType.Room;
            }
            else if (_mapWidth > _mapHeight)
            {
                StageDir = StageDirType.Horizontal;
            }
            else if (_mapWidth < _mapHeight)
            {
                StageDir = StageDirType.Vertical;
            }

            //背景色
            _backColorSwitchTimer.Running = false;
            _backColors[0] = Utility.GetColor(settings[index++]);
            _backColors[1] = Utility.GetColor(settings[index++]);
            _backColorSwitchTimes[0] = (int)(float.Parse(settings[index++]) / Game2.Frame);
            _backColorSwitchTimes[1] = (int)(float.Parse(settings[index++]) / Game2.Frame);

            //制限時間
            _timeLimit = (int)(float.Parse(settings[index++]) / Game2.Frame);
            Game2.Session.TimeLimit = _timeLimit;

            //BGM演奏開始
            _songName = $"Songs/{settings[index++]}";

            //暗闇ステージ
            _darkZoneSwitchTimer.Running = false;
            _darkZoneSwitch = false;
            _darkZone = bool.Parse(settings[index++]);
            _darkZoneSwitchTimes[0] = (int)(float.Parse(settings[index++]) / Game2.Frame);
            _darkZoneSwitchTimes[1] = (int)(float.Parse(settings[index++]) / Game2.Frame);

            //敵
            _enemyGenerator = new EnemyGenerator(Game2);
            _enemyGenerator.SetSpawn(0, bool.Parse(settings[index++]));
            _enemyGenerator.SetSpawn(1, bool.Parse(settings[index++]));
            _enemyGenerator.SetSpawn(2, bool.Parse(settings[index++]));
            _enemyGenerator.SetSpawn(3, bool.Parse(settings[index++]));
            _enemyGenerator.SetSpawn(4, bool.Parse(settings[index++]));
            _enemyGenerator.SetSpawn(5, bool.Parse(settings[index++]));
            _enemyGenerator.SetSpawn(6, bool.Parse(settings[index++]));
            _enemyGenerator.SetSpawn(7, bool.Parse(settings[index++]));
            _enemyGenerator.SetSpawn(8, bool.Parse(settings[index++]));
            _enemyGenerator.SetSpawn(9, bool.Parse(settings[index++]));
            _enemyGenerator.SetSpawn(10, bool.Parse(settings[index++]));
        }

        /// <summary>
        /// ゲーム開始
        /// </summary>
        public void GameStart()
        {
            FocusCamera2D();
            Game2.MusicPlayer.PlaySong(_songName);
            _enemyGenerator.BossFlag = false;
            Timer.Start(Game2.Session.TimeLimit);
        }

        public override void Update()
        {
            _lifeDisp.Update();
            _remainDisp.Update();
            _scoreDisp.Update();

            //タイマー制御
            _slow = !_slow;

            if (Game2.Session.Inventory.HasTimeItem() && _slow)
            {
                //タイマーアイテム所持の場合、2フレームに一回だけタイマーの更新を行う
            }
            else
            {
                _ = Timer.Update();
            }

            if (!Timer.Running)
            {
                if (Player.ObjectStatus == PhysicsObjectStatus.Normal)
                {
                    EffectObjs.Add(new PopupMessage(Game2, Player.Position.X, Player.Position.Y, "TIME OVER"));
                    Player.ObjectStatus = PhysicsObjectStatus.Dead;
                    Game2.Session.Inventory.SetItem("Shoes", false);
                    Game2.Session.Inventory.SetItem("Time", false);
                }
            }

            _timeLimitDisp.Update();
            _timeLimitDisp.Value = Timer.GetSecond();

            foreach (GameObject item in ItemObjs)
            {
                item.Update();
            }

            int count = EffectObjs.Count - 1;

            for (int i = count; i >= 0; i--)
            {
                EffectObjs[i].Update();

                if (EffectObjs[i].ObjectStatus == PhysicsObjectStatus.Remove)
                {
                    EffectObjs[i].Removed();
                    EffectObjs.RemoveAt(i);
                }
            }

            count = PhysicsObjs.Count - 1;

            for (int i = count; i >= 0; i--)
            {
                PhysicsObjs[i].Update();

                if (PhysicsObjs[i].ObjectStatus == PhysicsObjectStatus.Remove)
                {
                    PhysicsObjs[i].Removed();
                    PhysicsObjs.RemoveAt(i);
                }
            }

            count = NearMapObjs.Count - 1;

            for (int i = count; i >= 0; i--)
            {
                NearMapObjs[i].Update();
            }

            _enemyGenerator.Update();

            FocusCamera2D();
            _ = _backColorSwitchTimer.Update();
            _ = _darkZoneSwitchTimer.Update();
        }

        private void FocusXCamera2D()
        {
            if (Player.Rectangle.Left < Game2.Camera2D.Position.X + 100)
            {
                int val = Player.Rectangle.Left - 100;
                val = MathHelper.Clamp(val, 0, (_mapWidth * 16) - Game2.Width);
                Game2.Camera2D.FocusX(val);
            }
            else if (Game2.Camera2D.Position.X + 156 < Player.Rectangle.Right)
            {
                int val = Player.Rectangle.Right - 156;
                val = MathHelper.Clamp(val, 0, (_mapWidth * 16) - Game2.Width);
                Game2.Camera2D.FocusX(val);
            }
        }

        private void FocusYCamera2D()
        {
            if (Player.Rectangle.Top < Game2.Camera2D.Position.Y + 100)
            {
                int val = Player.Rectangle.Top - 100;
                val = MathHelper.Clamp(val, 0, (_mapHeight * 16) - Game2.Height);
                Game2.Camera2D.FocusY(val);
            }
            else if (Game2.Camera2D.Position.Y + 156 < Player.Rectangle.Bottom)
            {
                int val = Player.Rectangle.Bottom - 156;
                val = MathHelper.Clamp(val, 0, (_mapHeight * 16) - Game2.Height);
                Game2.Camera2D.FocusY(val);
            }
        }

        /// <summary>
        /// キャラクターが上下方向の画面外に出たか確認する
        /// </summary>
        /// <param name="y">キャラクターのY座標</param>
        /// <returns>画面外に出たか</returns>
        public bool IsOutOfMapY(float y)
        {
            return y > OutOfMapY || y < -100;
        }

        /// <summary>
        /// カメラの位置を更新する
        /// </summary>
        public override void FocusCamera2D()
        {
            if (StageDir == StageDirType.Horizontal)
            {
                FocusXCamera2D();
            }
            else if (StageDir == StageDirType.Vertical)
            {
                FocusYCamera2D();
            }
            else
            {
                FocusXCamera2D();
                FocusYCamera2D();
            }

            //視界内のマップに対してのみ物理演算を行う
            _sight.X = (int)Game2.Camera2D.Position.X - 16;
            _sight.Y = (int)Game2.Camera2D.Position.Y - 16;

            foreach (GameObject item in _mapObjs)
            {
                //移動床は画面外でも動かす
                if (item.ObjectKind != GameObjectKinds.MovingFloor && Rectangle.Intersect(item.Rectangle, _sight).IsEmpty)
                {
                    if (NearMapObjs.Contains(item))
                    {
                        _ = NearMapObjs.Remove(item);
                    }

                    item.Outside();
                }
                else
                {
                    if (!NearMapObjs.Contains(item))
                    {
                        NearMapObjs.Add(item);
                    }
                }
            }

            foreach (GameObject item in _backObjs)
            {
                if (Rectangle.Intersect(item.Rectangle, _sight).IsEmpty)
                {
                    if (_nearBackObjs.Contains(item))
                    {
                        _ = _nearBackObjs.Remove(item);
                    }

                    item.Outside();
                }
                else
                {
                    if (!_nearBackObjs.Contains(item))
                    {
                        _nearBackObjs.Add(item);
                    }
                }
            }

            foreach (GameObject item in _frontObjs)
            {
                if (Rectangle.Intersect(item.Rectangle, _sight).IsEmpty)
                {
                    if (_nearFrontObjs.Contains(item))
                    {
                        _ = _nearFrontObjs.Remove(item);
                    }

                    item.Outside();
                }
                else
                {
                    if (!_nearFrontObjs.Contains(item))
                    {
                        _nearFrontObjs.Add(item);
                    }
                }
            }
        }
    }
}
