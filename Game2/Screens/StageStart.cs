using Game2.Inputs;
using Game2.Managers;
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
        private readonly List<Rectangle?> _icons = new List<Rectangle?>();

        private bool _keyFlag = true;

        internal StageStart(ref Game2 game2, ref SpriteFont font) : base(ref game2, ref font)
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
                _icons.Add(Game2.Textures.GetTexture($"ItemDoubleScore"));
            }

            if (Game2.Inventory.HasFinderItem())
            {
                _icons.Add(Game2.Textures.GetTexture($"ItemFinder"));
            }

            if (Game2.Inventory.HasShieldItem())
            {
                _icons.Add(Game2.Textures.GetTexture($"ItemShield"));
            }

            if (Game2.Inventory.HasTimeItem())
            {
                _icons.Add(Game2.Textures.GetTexture($"ItemTime"));
            }

            if (Game2.Inventory.HasLightItem())
            {
                _icons.Add(Game2.Textures.GetTexture($"ItemLight"));
            }

            if (Game2.Inventory.HasSwordItem())
            {
                _icons.Add(Game2.Textures.GetTexture($"ItemSword"));
            }

            if (Game2.Inventory.HasShoesItem())
            {
                _icons.Add(Game2.Textures.GetTexture($"ItemShoes"));
            }

            if (Game2.Inventory.HasTripleShotItem())
            {
                _icons.Add(Game2.Textures.GetTexture($"ItemTripleShot"));
            }

            if (Game2.Inventory.HasHighJumpItem())
            {
                _icons.Add(Game2.Textures.GetTexture($"ItemHighJump"));
            }

            Game2.MusicPlayer.PlaySong($"Songs/BGM4");
        }

        internal override void Timeup()
        {
            Game2.Scheduler.SetSchedule(Schedules.GameStart);
        }

        internal override void Update(ref Vector2 offset, ref GameTime gametime)
        {
            //一度離すのを確認してからスキップ入力を受け付ける
            //絶対離したくない人が、そのまま押していてもタイムアップする。
            if (_keyFlag && Game2.GameCtrl.IsRelease(ButtonNames.Fire))
            {
                _keyFlag = false;
            }
            else if (Game2.GameCtrl.IsClick(ButtonNames.Fire))
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
                spriteBatch.Draw(Game2.Images, new Vector2(20 + 20 * i, 150), _icons[i], Color.White);
            }
        }
    }
}
