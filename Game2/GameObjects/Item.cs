using Microsoft.Xna.Framework;

namespace Game2.GameObjects
{
    /// <summary>
    /// アイテム
    /// </summary>
    public class Item : GameObject
    {
        /// <summary>
        /// アイテム可視性
        /// </summary>
        public ObjectVisibility Visibility;

        /// <summary>
        /// アイテム名
        /// </summary>
        public string Name;

        /// <summary>
        /// アイテム画像
        /// </summary>
        public Rectangle? ItemImg;

        /// <summary>
        /// 隠しアイテム用ダミー画像
        /// </summary>
        public Rectangle? DummyImg;

        /// <summary>
        /// ステージ番号
        /// </summary>
        private readonly int _stageNo;

        /// <summary>
        /// アイテム番号
        /// </summary>
        private readonly int _itemNo;

        public Item(Game2 game2, float x, float y, int stageNo, int itemNo, string name, bool hidden, string dummy) : base(game2, x, y)
        {
            ObjectKind = GameObjectKinds.Item;
            Name = name;
            _stageNo = stageNo;
            _itemNo = itemNo;
            ItemImg = Game2.Textures.GetTexture($"Item{name}");

            Visibility = hidden ? ObjectVisibility.Hidden : ObjectVisibility.Normal;

            if (dummy != "Null")
            {
                DummyImg = Game2.Textures.GetTexture("" + dummy);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Visibility == ObjectVisibility.Hidden)
            {
                Img = DummyImg;
            }
            else if (Visibility == ObjectVisibility.Disable)
            {
                Img = null;
            }
            else if (Visibility == ObjectVisibility.Normal)
            {
                Img = ItemImg;
            }
        }

        /// <summary>
        /// ドアID「ステージ番号-ドア番号」を得る。
        /// </summary>
        /// <returns>宝箱ID</returns>
        public string GetItemID()
        {
            return $"{_stageNo}-{_itemNo}";
        }
    }
}
