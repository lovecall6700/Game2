using Game2.Inputs;
using Game2.Managers;
using Game2.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game2.Screens
{
    /// <summary>
    /// エンディング画面
    /// </summary>
    public class EndingScreen : TimerScreen
    {

        /// <summary>
        /// 画面の状態
        /// </summary>
        private int _state = 0;

        /// <summary>
        /// 画像
        /// </summary>
        private readonly ImageList _img = new ImageList();

        /// <summary>
        /// 2ndメッセージ
        /// </summary>
        public MenuItem SecondMsg;

        /// <summary>
        /// 3rdメッセージ
        /// </summary>
        public MenuItem ThirdMsg;

        /// <summary>
        /// 画像の位置
        /// </summary>
        private readonly List<Vector2> _position = new List<Vector2>();

        /// <summary>
        /// 常時スキップを受け付けるか
        /// </summary>
        public bool AlwaysSkip = false;

        public EndingScreen(Game2 game2) : base(game2)
        {
            string baseName = FileName();

            for (int i = 0; i < NumOfImage(); i++)
            {
                _img.AddImage(Game2.Textures.GetTexture(baseName + (i + 1)));
                _position.Add(new Vector2(0, 256 + i * 128));
            }

            string msg1 = Msg1();
            float msg1Sclae = Msg1Scale();
            Item = new MenuItem(new Vector2(128, 128) - GetMsgSize(msg1, msg1Sclae) / 2, msg1, msg1Sclae);
            AddSecondMsg();
            AddThirdMsg();
            Game2.MusicPlayer.PlaySong(BgmName());
            Timer.Start(WaitTime1(), true);
        }

        public virtual void AddSecondMsg()
        {
            int sc = Game2.GetScore();
            int hs = Game2.Session.HighScore;
            string score = $"SCORE:{sc}";
            SecondMsg = new MenuItem(new Vector2(128, 200) - GetMsgSize(score, 0.5f) / 2, score, 0.5f);

            if (sc == hs)
            {
                SecondMsg.Color = Color.Red;
            }
            else
            {
                SecondMsg.Color = Color.White;
            }
        }

        public virtual void AddThirdMsg()
        {
            if (Game2.Session.EnableTime)
            {
                string time = $"TIME:{Game2.Session.CalcTime()}ms";
                ThirdMsg = new MenuItem(new Vector2(128, 220) - GetMsgSize(time, 0.5f) / 2, time, 0.5f)
                {
                    Color = Color.White
                };
            }
        }

        public virtual string FileName()
        {
            return "End";
        }

        public virtual int NumOfImage()
        {
            return Game2.Session.StageNo == 24 ? 6 : 5;
        }

        public virtual string Msg1()
        {
            return "Congratulations!!";
        }

        public virtual float Msg1Scale()
        {
            return 1f;
        }

        public virtual string Msg2()
        {
            return Game2.Session.StageNo == 24 ? "Fin!!" : "Fin.";
        }

        public virtual float Msg2Scale()
        {
            return 3f;
        }

        public virtual float WaitTime1()
        {
            return 2000f;
        }

        public virtual float WaitTime2()
        {
            return 30000f;
        }

        public virtual string BgmName()
        {
            return "Songs/BGM7";
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_state == 0)
            {
            }
            else if (_state == 1)
            {
                Item.Position.Y -= 1;
                Vector2 v;

                for (int i = 0; i < NumOfImage(); i++)
                {
                    v = _position[i];
                    v.Y -= 1;
                    _position[i] = v;
                }

                if (_position[NumOfImage() - 1].Y == 0)
                {
                    //ダミーの時間を上書きする
                    Timer.Start(5000, true);
                }
            }
            else if (_state == 2)
            {
            }

            if (AlwaysSkip || _state == 2)
            {
                if (Game2.GameCtrl.IsClick(ButtonNames.Fire))
                {
                    _state = 2;
                    Timer.Running = false;
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_state == 0)
            {
                base.Draw(gameTime, spriteBatch);
            }
            else if (_state == 1)
            {
                base.Draw(gameTime, spriteBatch);

                for (int i = 0; i < NumOfImage(); i++)
                {
                    spriteBatch.Draw(Game2.Images, _position[i], _img.GetImage(i), Color.White);
                }
            }
            else if (_state == 2)
            {
                base.Draw(gameTime, spriteBatch);

                if (SecondMsg != null)
                {
                    SecondMsg.Draw(spriteBatch, Game2.Font);
                }

                if (ThirdMsg != null)
                {
                    ThirdMsg.Draw(spriteBatch, Game2.Font);
                }
            }
        }

        public override void Timeup()
        {
            if (_state == 0)
            {
                _state = 1;
                //ダミーの時間を長めにセット
                Timer.Start(1000000, true);
            }
            else if (_state == 1)
            {
                _state = 2;
                string msg2 = Msg2();
                float msg2Sclae = Msg2Scale();
                Item = new MenuItem(new Vector2(128, 128) - GetMsgSize(msg2, msg2Sclae) / 2, msg2, msg2Sclae);
                Timer.Start(WaitTime2(), true);
            }
            else if (_state == 2)
            {
                Game2.Scheduler.SetSchedule(Schedules.Title);
            }
        }
    }
}
