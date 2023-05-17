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

        /// <summary>
        /// スコアを画面に表示するか
        /// </summary>
        private readonly HighScoreDisplay _scoreDisp;

        public StageStart(Game2 game2) : base(game2)
        {
            Timer.Start(126);
            string msg = Game2.Session.StageNo == Session.StartStageNo && Game2.Session.DoorNo == Session.StartDoorNo
                ? $"Stage {Game2.Session.StageNo} Start!!"
                : $"Stage {Game2.Session.StageNo} - Door {Game2.Session.DoorNo + 1} Start!!";
            Vector2 v = GetMsgSize(msg, 1f);
            v.Y = 0;
            Item = new MenuItem(new Vector2(128, 100) - (v / 2), msg, 1f);

            //アイテムの所有を確認する
            if (Game2.Session.Inventory.HasDoubleScoreItem())
            {
                _icons.Add(Game2.Textures.GetTexture($"ItemDoubleScore"));
            }

            if (Game2.Session.Inventory.HasFinderItem())
            {
                _icons.Add(Game2.Textures.GetTexture($"ItemFinder"));
            }

            if (Game2.Session.Inventory.HasShieldItem())
            {
                _icons.Add(Game2.Textures.GetTexture($"ItemShield"));
            }

            if (Game2.Session.Inventory.HasTimeItem())
            {
                _icons.Add(Game2.Textures.GetTexture($"ItemTime"));
            }

            if (Game2.Session.Inventory.HasLightItem())
            {
                _icons.Add(Game2.Textures.GetTexture($"ItemLight"));
            }

            if (Game2.Session.Inventory.HasSwordItem())
            {
                _icons.Add(Game2.Textures.GetTexture($"ItemSword"));
            }

            if (Game2.Session.Inventory.HasShoesItem())
            {
                _icons.Add(Game2.Textures.GetTexture($"ItemShoes"));
            }

            if (Game2.Session.Inventory.HasTripleShotItem())
            {
                _icons.Add(Game2.Textures.GetTexture($"ItemTripleShot"));
            }

            if (Game2.Session.Inventory.HasHighJumpItem())
            {
                _icons.Add(Game2.Textures.GetTexture($"ItemHighJump"));
            }

            Game2.MusicPlayer.PlaySong($"Songs/BGM4");
            _scoreDisp = new HighScoreDisplay(game2);
        }

        public override void Timeup()
        {
            Game2.Scheduler.SetSchedule(Schedules.GameStart);
        }

        public override void Update()
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            for (int i = 0; i < _icons.Count; i++)
            {
                spriteBatch.Draw(Game2.Images, new Vector2(20 + (20 * i), 150), _icons[i], Color.White);
            }

            _scoreDisp.Draw(spriteBatch);
        }
    }
}
