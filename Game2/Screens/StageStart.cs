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
    public class StageStart : TimerScreen
    {
        /// <summary>
        /// アイテムアイコン
        /// </summary>
        private readonly List<Rectangle?> _icons = new List<Rectangle?>();

        private bool _keyFlag = true;

        public StageStart(Game2 game2) : base(game2)
        {
            Timer.Start(126);
            string msg = Game2.Session.StageNo == Game2.StartStageNo && Game2.Session.DoorNo == Game2.StartDoorNo
                ? $"Stage {Game2.Session.StageNo} Start!!"
                : $"Stage {Game2.Session.StageNo} - Door {Game2.Session.DoorNo + 1} Start!!";
            Vector2 v = GetMsgSize(msg, 1f);
            v.Y = 0;
            Item = new MenuItem(new Vector2(128, 100) - (v / 2), msg, 1f);

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

        public override void Timeup()
        {
            Game2.Scheduler.SetSchedule(Schedules.GameStart);
        }

        public override void Update(GameTime gameTime)
        {
            if (WaitTimer.Update())
            {
                return;
            }

            if (!Timer.Update())
            {
                Timeup();
            }

            //一度離すのを確認してから入力を受け付ける
            if (_keyFlag)
            {
                if (Game2.GameCtrl.IsRelease(ButtonNames.Fire))
                {
                    _keyFlag = false;
                }

                return;
            }

            if (Game2.GameCtrl.IsClick(ButtonNames.Fire))
            {
                //強制定期にタイムアップを発生させる
                Timer.Running = false;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            for (int i = 0; i < _icons.Count; i++)
            {
                spriteBatch.Draw(Game2.Images, new Vector2(20 + (20 * i), 150), _icons[i], Color.White);
            }
        }
    }
}
