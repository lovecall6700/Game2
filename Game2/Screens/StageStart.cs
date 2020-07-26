using Game2.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game2.Screens
{
    /// <summary>
    /// ステージ開始画面
    /// </summary>
    internal class StageStart : TimerScreen
    {
        /// <summary>
        /// アイテムアイコン
        /// </summary>
        private readonly List<Texture2D> _icons = new List<Texture2D>();

        internal StageStart(Game2 game2, SpriteFont font) : base(game2, font)
        {
            Timer.Start(4200, true);
            string msg;

            if (Game2.Session.StageNo == Game2.StartStageNo && Game2.Session.DoorNo == Game2.StartDoorNo)
            {
                msg = $"Stage {Game2.Session.StageNo} Start!!";
            }
            else
            {
                msg = $"Stage {Game2.Session.StageNo} - Door {Game2.Session.DoorNo + 1} Start!!";
            }

            Vector2 v = GetMsgSize(msg, 1f);
            v.Y = 0;
            Item = new MenuItem(new Vector2(128, 100) - v / 2, msg, 1f);

            //アイテムの所有を確認する
            if (Game2.Inventory.HasDoubleScoreItem())
            {
                _icons.Add(Game2.Textures.GetTexture($"Images/ItemDoubleScore"));
            }

            if (Game2.Inventory.HasFinderItem())
            {
                _icons.Add(Game2.Textures.GetTexture($"Images/ItemFinder"));
            }

            if (Game2.Inventory.HasShieldItem())
            {
                _icons.Add(Game2.Textures.GetTexture($"Images/ItemShield"));
            }

            if (Game2.Inventory.HasTimeItem())
            {
                _icons.Add(Game2.Textures.GetTexture($"Images/ItemTime"));
            }

            if (Game2.Inventory.HasLightItem())
            {
                _icons.Add(Game2.Textures.GetTexture($"Images/ItemLight"));
            }

            if (Game2.Inventory.HasSwordItem())
            {
                _icons.Add(Game2.Textures.GetTexture($"Images/ItemSword"));
            }

            if (Game2.Inventory.HasShoesItem())
            {
                _icons.Add(Game2.Textures.GetTexture($"Images/ItemShoes"));
            }

            if (Game2.Inventory.HasTripleShotItem())
            {
                _icons.Add(Game2.Textures.GetTexture($"Images/ItemTripleShot"));
            }

            if (Game2.Inventory.HasHighJumpItem())
            {
                _icons.Add(Game2.Textures.GetTexture($"Images/ItemHighJump"));
            }

            Game2.MusicPlayer.PlaySong($"Songs/BGM4");
        }

        internal override void Timeup()
        {
            Game2.Scheduler.GameStart();
        }

        internal override void Update(ref Vector2 offset, ref GameTime gametime)
        {
            if (Game2.GameCtrl.IsClick(Managers.KeyName.Fire))
            {
                //強制定期にタイムアップを発生させる
                Timer.Running = false;
            }

            base.Update(ref offset, ref gametime);
        }

        internal override void Draw(ref Vector2 offset, ref GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            base.Draw(ref offset, ref gameTime, ref spriteBatch);

            for (int i = 0; i < _icons.Count; i++)
            {
                spriteBatch.Draw(_icons[i], new Vector2(20 + 20 * i, 150), Color.White);
            }
        }
    }
}
