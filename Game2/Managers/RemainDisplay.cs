using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Managers
{
    /// <summary>
    /// 残機管理
    /// </summary>
    public class RemainDisplay : DigitalDisplay
    {
        /// <summary>
        /// エクステンド回数
        /// </summary>
        private int _extendCount;

        /// <summary>
        /// 前回エクステンドスコア
        /// </summary>
        private int _lastExtendScore;

        /// <summary>
        /// エクステンドスコア
        /// </summary>
        private readonly int[] _extendScores = new[] { 10000, 20000, 30000, 50000 };

        public RemainDisplay(Game2 game2, GraphicsDevice device) : base(game2, device)
        {
            Format = "REMAIN {0:0}";
        }

        public override void Initialize(GraphicsDevice device)
        {
            base.Initialize(device);
            Position = new Vector2(2, 5);
        }

        public void TitleToInitialStart()
        {
            Value = 2;
            _lastExtendScore = 0;
            _extendCount = 0;
        }

        public void GameoverRetry()
        {
            Value = 2;
            _lastExtendScore = 0;
            _extendCount = 0;
        }

        public void TitleContinue()
        {
            Value = 2;
            _lastExtendScore = 0;
            _extendCount = 0;
        }

        /// <summary>
        /// 得点を獲得
        /// </summary>
        /// <param name="score">獲得した得点</param>
        public void AddScore(int score)
        {
            _lastExtendScore += score;

            while (true)
            {
                if (_extendScores[_extendCount] <= _lastExtendScore)
                {
                    _lastExtendScore -= _extendScores[_extendCount];
                    _extendCount = MathHelper.Clamp(_extendCount + 1, 0, _extendScores.Length - 1);
                    Value = MathHelper.Clamp(Value + 1, 0, 10);
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
            Value--;

            return Value < 0;
        }
    }
}
