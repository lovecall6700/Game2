using Game2.Utilities;
using Microsoft.Xna.Framework;

namespace Game2.GameObjects
{
    /// <summary>
    /// Enemy
    /// 
    /// 敵0(直進、壁で反転、穴に落下): EnemyDog
    /// 敵1(直進、飛行): EnemyBird
    /// 敵2(動かない): EnemyNeedle
    /// 敵3(直進、ジャンプ、壁で反転、穴に落下): EnemyJumpDog
    /// 敵4(下からジャンプして出現、壁透過、放物線): EnemyJumpFish
    /// 敵5(飛行、追尾): EnemyHomingFire
    /// 敵6(直進、壁で反転、穴に落下、プレーヤーを左右攻撃): EnemyUPeopleBasic
    /// 敵7(直進、壁でジャンプ or 反転、穴を飛び越える、プレーヤーを左右攻撃): EnemyUPeopleJump
    /// 敵8(直進、壁でジャンプ or 反転、穴を飛び越える、プレーヤーを八方攻撃): EnemyUPeople8Shots
    /// 敵9(直進、壁でジャンプ or 反転、穴に落下、プレーヤーを左右攻撃、高耐久): EnemyUPeopleLife
    /// 敵10(ボス): EnemyBoss
    /// </summary>
    internal class Enemy : PhysicsObject
    {
        /// <summary>
        /// 寿命タイマー
        /// </summary>
        private readonly Timer _timer = new Timer();

        /// <summary>
        /// 寿命までの時間
        /// </summary>
        internal float LifeTime = 10000f;

        internal Enemy(Game2 game2, float x, float y) : base(game2, x, y)
        {
            ObjectKind = GameObjectKind.Enemy;
            DeadSE = "SoundEffects/EnemyDead";
            DamageSE = "SoundEffects/EnemyDamage";
            //一定時間たったら勝手に死ぬ
            _timer.Start(LifeTime, true);
        }

        /// <summary>
        /// 寿命を計算する
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        internal void UpdateLifeTime(ref GameTime gameTime)
        {
            _timer.Update(ref gameTime);

            if (!_timer.Running)
            {
                ObjectStatus = PhysicsObjectStatus.Dead;
            }

            if (ObjectStatus == PhysicsObjectStatus.Damage)
            {
                ObjectStatus = PhysicsObjectStatus.Normal;
            }
            else if (ObjectStatus == PhysicsObjectStatus.Dead)
            {
                OnlyGravity();
            }
        }

        /// <summary>
        /// プレーヤーを攻撃する
        /// </summary>
        internal virtual void AttackPlayer()
        {
            if (Game2.Inventory.HasShieldItem())
            {
                return;
            }

            Player p = Game2.PlaySc.Player;

            if (!Rectangle.Intersect(p.Rectangle, Rectangle).IsEmpty)
            {
                p.Damage(Attack);
            }
        }
    }
}
