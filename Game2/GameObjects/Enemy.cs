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
        private readonly Timer _lifeTimer = new Timer();

        /// <summary>
        /// 寿命までの時間
        /// </summary>
        internal float LifeTime = 10000f;

        /// <summary>
        /// 寿命を使うか
        /// </summary>
        internal bool UseLifeTime = true;

        /// <summary>
        /// 発砲タイマー
        /// </summary>
        private readonly Timer _shotTimer = new Timer();

        /// <summary>
        /// 発砲までの時間
        /// </summary>
        internal float ShotTime = 10000f;

        internal Enemy(Game2 game2, float x, float y) : base(game2, x, y)
        {
            ObjectKind = GameObjectKind.Enemy;
            DeadSE = "SoundEffects/EnemyDead";
            DamageSE = "SoundEffects/EnemyDamage";
            //一定時間たったら勝手に死ぬ
            _lifeTimer.Start(LifeTime, true);
            _shotTimer.Start(ShotTime, true);
        }

        internal override void Update(ref GameTime gameTime)
        {
            UpdateLifeTime(ref gameTime);
            RecoveryDamage(ref gameTime);

            if (ObjectStatus == PhysicsObjectStatus.Dead)
            {
                OnlyGravity();
                return;
            }
            else if (ObjectStatus == PhysicsObjectStatus.Damage)
            {
                return;
            }

            if (MoveLeftOrRight(ref gameTime))
            {
                TouchWithWall();
            }

            JumpAndGravity(ref gameTime);
            AttackPlayer();
            UpdateShotTime(ref gameTime);
            FinallyUpdate(ref gameTime);
            UpdateAnimation();
        }

        /// <summary>
        /// 寿命を計算する
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        internal void UpdateLifeTime(ref GameTime gameTime)
        {
            if (!UseLifeTime)
            {
                return;
            }
            else if (!_lifeTimer.Update(ref gameTime))
            {
                ObjectStatus = PhysicsObjectStatus.Dead;
            }
        }

        internal void UpdateShotTime(ref GameTime gameTime)
        {
            if (!_shotTimer.Update(ref gameTime))
            {
                Shot();
                _shotTimer.Start(ShotTime, true);
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

        internal virtual void TouchWithWall()
        {
        }

        internal virtual void Shot()
        {
        }

        internal override void Outside()
        {
            ObjectStatus = PhysicsObjectStatus.Remove;
        }

        internal virtual void FinallyUpdate(ref GameTime gameTime)
        {

        }
    }
}
