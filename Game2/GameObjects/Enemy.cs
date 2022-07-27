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
    public class Enemy : PhysicsObject
    {
        /// <summary>
        /// 寿命タイマー
        /// </summary>
        private readonly Timer _lifeTimer = new Timer();

        /// <summary>
        /// 寿命までの時間
        /// </summary>
        public int LifeTime = 300;

        /// <summary>
        /// 寿命を使うか
        /// </summary>
        public bool UseLifeTime = true;

        /// <summary>
        /// 発砲タイマー
        /// </summary>
        private readonly Timer _shotTimer = new Timer();

        /// <summary>
        /// 発砲までの時間
        /// </summary>
        public int ShotTime = 300;

        public Enemy(Game2 game2, float x, float y) : base(game2, x, y)
        {
            ObjectKind = GameObjectKinds.Enemy;
            DeadSE = "SoundEffects/EnemyDead";
            DamageSE = "SoundEffects/EnemyDead";
            //一定時間たったら勝手に死ぬ
            _lifeTimer.Start(LifeTime);
            _shotTimer.Start(ShotTime);
        }

        public override void Update(GameTime gameTime)
        {
            UpdateLifeTime(gameTime);
            RecoveryDamage(gameTime);

            if (ObjectStatus == PhysicsObjectStatus.Dead)
            {
                OnlyGravity();
                return;
            }

            if (MoveLeftOrRight(gameTime))
            {
                TouchWithWall();
            }

            JumpAndGravity(gameTime);
            AttackPlayer();
            UpdateShotTime(gameTime);
            FinallyUpdate(gameTime);
            UpdateAnimation();
        }

        public override void Damaged()
        {
            base.Damaged();
            Velocity = new Vector2(-10 * Direction, -5);
        }

        /// <summary>
        /// 寿命を計算する
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public void UpdateLifeTime(GameTime gameTime)
        {
            if (!UseLifeTime)
            {
                return;
            }
            else if (!_lifeTimer.Update())
            {
                ObjectStatus = PhysicsObjectStatus.Dead;
            }
        }

        public void UpdateShotTime(GameTime gameTime)
        {
            if (!_shotTimer.Update())
            {
                Shot();
                _shotTimer.Start(ShotTime);
            }
        }

        /// <summary>
        /// プレーヤーを攻撃する
        /// </summary>
        public virtual void AttackPlayer()
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

        public virtual void TouchWithWall()
        {
        }

        public virtual void Shot()
        {
        }

        public override void Outside()
        {
            ObjectStatus = PhysicsObjectStatus.Remove;
        }

        public virtual void FinallyUpdate(GameTime gameTime)
        {
        }
    }
}
