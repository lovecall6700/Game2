using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Managers
{
    /// <summary>
    /// 残機管理
    /// </summary>
    internal class RemainDisplay : DigitalDisplay
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

        internal RemainDisplay(ref Game2 game2, ref SpriteFont font, GraphicsDevice device) : base(ref game2, ref font, device)
        {
            Format = "REMAIN {0:0}";
        }

        internal override void Initialize(GraphicsDevice device)
        {
            base.Initialize(device);
            Position = new Vector2(2, 5);
        }

        internal void TitleToInitialStart()
        {
            Value = 2;
            _lastExtendScore = 0;
            _extendCount = 0;
        }

        internal void GameoverRetry()
        {
            Value = 2;
            _lastExtendScore = 0;
            _extendCount = 0;
        }

        internal void TitleContinue()
        {
            Value = 2;
            _lastExtendScore = 0;
            _extendCount = 0;
        }

        /// <summary>
        /// 得点を獲得
        /// </summary>
        /// <param name="score">獲得した得点</param>
        internal void AddScore(int score)
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
        internal bool Miss()
        {
            Value--;

            if (Value < 0)
            {
                return true;
            }

            return false;
        }
    }
}
