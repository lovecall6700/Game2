using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.GameObjects
{
    /// <summary>
    /// 扉
    /// </summary>
    public class Door : GameObject
    {
        /// <summary>
        /// ステージ番号
        /// </summary>
        public readonly int StageNo;

        /// <summary>
        /// ドア番号
        /// </summary>
        public readonly int DoorNo;

        /// <summary>
        /// ドアタイプ
        /// </summary>
        public ObjectVisibility Visibility;

        /// <summary>
        /// ドアの移動先ステージ
        /// </summary>
        public int DestStageNo;

        /// <summary>
        /// ドアの移動先ドア番号
        /// </summary>
        public int DestDoorNo;

        /// <summary>
        /// ドア下段の座標
        /// </summary>
        private Vector2 _downOffset = new Vector2(0, 16);

        /// <summary>
        /// 閉じたテクスチャ
        /// </summary>
        private readonly Rectangle? _closeImg = null;

        /// <summary>
        /// 不可視・隠し扉のダミーテクスチャ上段
        /// </summary>
        private readonly Rectangle? _dummyImgUp = null;

        /// <summary>
        /// 不可視・隠し扉のダミーテクスチャ下段
        /// </summary>
        private readonly Rectangle? _dummyImgDown = null;

        /// <summary>
        /// 扉
        /// </summary>
        /// <param name="game2">Game2</param>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <param name="doorNo">ドア番号</param>
        /// <param name="doorType">ドアタイプ</param>
        /// <param name="destStage">ドアの移動先ステージ</param>
        /// <param name="destNo">ドアの移動先ドア番号</param>
        /// <param name="dummyUp">不可視・隠し扉の上段テクスチャ名</param>
        /// <param name="dummyDown">不可視・隠し扉の下段テクスチャ名</param>
        public Door(Game2 game2, float x, float y, int stageNo, int doorNo, string doorType, int destStage, int destNo, string dummyUp, string dummyDown) : base(game2, x, y)
        {
            ObjectKind = GameObjectKinds.Door;
            DoorNo = doorNo;
            StageNo = stageNo;
            DestStageNo = destStage;
            DestDoorNo = destNo;
            _closeImg = Game2.Textures.GetTexture("DoorClose");
            SetSize(16, 32);

            Visibility = doorType == "Hidden"
                ? ObjectVisibility.Hidden
                : doorType == "Invisible"
                    ? ObjectVisibility.Invisible
                    : doorType == "Disable" ? ObjectVisibility.Disable : ObjectVisibility.Normal;

            if (dummyUp != "Null")
            {
                _dummyImgUp = Game2.Textures.GetTexture("" + dummyUp);
            }

            if (dummyDown != "Null")
            {
                _dummyImgDown = Game2.Textures.GetTexture("" + dummyDown);
            }
        }

        /// <summary>
        /// ドアID「ステージ番号-ドア番号」を得る。
        /// </summary>
        /// <returns>宝箱ID</returns>
        public string GetDoorID()
        {
            return $"{StageNo}-{DoorNo}";
        }

        public override void Update(GameTime gameTime)
        {
            if (Visibility == ObjectVisibility.Normal)
            {
                //扉は閉じている
                Img = _closeImg;
            }
            else
            {
                Img = null;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Visibility == ObjectVisibility.Invisible || Visibility == ObjectVisibility.Hidden)
            {
                //不可視・隠し扉はダミーのテクスチャを描画する
                if (_dummyImgUp != null)
                {
                    spriteBatch.Draw(Game2.Images, Position, _dummyImgUp, Color.White);
                }

                if (_dummyImgDown != null)
                {
                    spriteBatch.Draw(Game2.Images, Position + _downOffset, _dummyImgDown, Color.White);
                }

                return;
            }

            base.Draw(gameTime, spriteBatch);
        }
    }
}
