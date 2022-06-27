using Microsoft.Xna.Framework;

namespace Game2.GameObjects
{
    /// <summary>
    /// 宝箱
    /// </summary>
    internal class TreasureBox : GameObject
    {
        /// <summary>
        /// ステージ番号
        /// </summary>
        private readonly int _stageNo;

        /// <summary>
        /// ドア番号
        /// </summary>
        private readonly int _treasureBoxNo;

        /// <summary>
        /// 宝箱タイプ
        /// </summary>
        internal ObjectVisibility Visibility;

        /// <summary>
        /// 得点
        /// </summary>
        internal int Score;

        /// <summary>
        /// 開いた宝箱のテクスチャ
        /// </summary>
        private readonly Rectangle? _openImg = null;

        /// <summary>
        /// 閉じた宝箱のテクスチャ
        /// </summary>
        private readonly Rectangle? _closeImg = null;

        /// <summary>
        /// 隠し宝箱のダミーテクスチャ
        /// </summary>
        private readonly Rectangle? _dummyImg = null;

        /// <summary>
        /// TreasureBox
        /// </summary>
        /// <param name="game2">Game2</param>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <param name="hidden">隠し宝箱</param>
        /// <param name="score">得点</param>
        /// <param name="dummy">隠し宝箱のダミーテクスチャ名</param>
        internal TreasureBox(ref Game2 game2, float x, float y, bool hidden, int score, string dummy, int stageNo, int treasureBoxNo) : base(ref game2, x, y)
        {
            ObjectKind = GameObjectKinds.TreasureBox;
            _stageNo = stageNo;
            _treasureBoxNo = treasureBoxNo;

            if (hidden)
            {
                Visibility = ObjectVisibility.Hidden;
            }
            else
            {
                Visibility = ObjectVisibility.Normal;
            }

            Score = score;
            _closeImg = Game2.Textures.GetTexture("TreasureBoxClose");
            _openImg = Game2.Textures.GetTexture("TreasureBoxOpen");
            Img = _closeImg;

            if (dummy != "Null")
            {
                _dummyImg = Game2.Textures.GetTexture("" + dummy);
            }
        }

        /// <summary>
        /// 宝箱ID「ステージ番号-宝箱番号」を得る。
        /// </summary>
        /// <returns>宝箱ID</returns>
        internal string GetTreasureBoxID()
        {
            return $"{_stageNo}-{_treasureBoxNo}";
        }

        internal override void Update(GameTime gameTime)
        {
            if (Visibility == ObjectVisibility.Hidden)
            {
                Img = _dummyImg;
            }
            else if (Visibility == ObjectVisibility.Open)
            {
                Img = _openImg;
            }
            else
            {
                Img = _closeImg;
            }
        }
    }
}
