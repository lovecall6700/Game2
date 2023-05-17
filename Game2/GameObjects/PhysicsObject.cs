using Game2.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game2.GameObjects
{
    /// <summary>
    /// 敵や動く床などの画面上を動く物体
    /// </summary>
    public class PhysicsObject : GameObject
    {
        //各種画像
        public ImageList RImg = new ImageList();
        public ImageList LImg = new ImageList();
        public ImageList LadderImg = new ImageList();
        public Rectangle? DeadImg = null;
        public Rectangle? DamageImg = null;

        //効果音
        public string DeadSE = null;
        public string DamageSE = null;

        /// <summary>
        /// 向いている方向
        /// </summary>
        public int Direction = 0;

        /// <summary>
        /// 常にアニメーションするか
        /// </summary>
        public bool AnimationAlways = false;

        /// <summary>
        /// ライフ
        /// </summary>
        public int Life = 1;

        /// <summary>
        /// 攻撃力
        /// </summary>
        public int Attack = 1;

        /// <summary>
        /// 左右方向の移動が制御されているか
        /// -1: 左方向
        /// 0: 無制御
        /// 1: 右方向
        /// </summary>
        public float ControlDirectionX = 0;

        /// <summary>
        /// 上下方向の移動が制御されているか
        /// -1: 上方向
        /// 0: 無制御
        /// 1: 下方向
        /// </summary>
        public float ControlDirectionY = 0;

        /// <summary>
        /// 左右方向の加速度
        /// </summary>
        public float AccelerationX = 3f;

        /// <summary>
        /// 空中の左右方向の加速度
        /// </summary>
        public float AirAccelerationX = 2f;

        /// <summary>
        /// 左右方向の最大・最小速度の絶対値
        /// </summary>
        public float MaxSpeedX = 8;

        /// <summary>
        /// 重力加速度
        /// </summary>
        public float Gravity = 1;

        /// <summary>
        /// ジャンプ加速度
        /// </summary>
        public int JumpAcceleration = 12;

        /// <summary>
        /// 最大・最小上下方向速度の絶対値
        /// </summary>
        public int MaxJumpSpeed = 15;

        /// <summary>
        /// 速度
        /// </summary>
        public Vector2 Velocity;

        /// <summary>
        /// 空中の摩擦を使用するか
        /// </summary>
        public bool UseAirFriction = false;

        /// <summary>
        /// 抵抗を受けるブロック
        /// </summary>
        public GameObject GroundBlock;

        /// <summary>
        /// オブジェクト状態
        /// </summary>
        public PhysicsObjectStatus ObjectStatus;

        /// <summary>
        /// ハシゴを使用するか
        /// </summary>
        public bool UseLadder = false;

        /// <summary>
        /// ハシゴの上にいるか
        /// </summary>
        public bool OnLadder = false;

        /// <summary>
        /// 落下で消滅するか
        /// </summary>
        public bool UseOutOfMapY = true;

        /// <summary>
        /// 連続ダメージ回避時間
        /// </summary>
        public int DamageTime = 9;

        /// <summary>
        /// 連続ダメージ回避タイマー
        /// </summary>
        public readonly Timer DamageTimer = new Timer();

        /// <summary>
        /// アニメーションするか
        /// </summary>
        public bool UseAnimation = true;

        public PhysicsObject(Game2 game2, float x, float y) : base(game2, x, y)
        {
            ObjectStatus = PhysicsObjectStatus.Normal;
        }

        public override void Update()
        {
        }

        /// <summary>
        /// ハシゴの処理
        /// ハシゴの上にいないなら通常のJumpAndGravity()を呼ぶ
        /// </summary>
        public void Ladder()
        {
            OnLadder = false;
            GameObject ladder = null;

            //ハシゴにいるか確認する
            foreach (GameObject o in Game2.PlaySc.NearMapObjs)
            {
                if (o.ObjectKind != GameObjectKinds.Ladder || o.ObjectKind == GameObjectKinds.Disable)
                {
                    continue;
                }
                else if (!Rectangle.Intersect(o.Rectangle, Rectangle).IsEmpty)
                {
                    OnLadder = true;
                    ladder = o;
                    break;
                }
            }

            //ハシゴの上にいないなら普通に重力を処理
            if (!OnLadder)
            {
                _ = JumpAndGravity();
                return;
            }

            //ハシゴの上にいる
            //上り下りの最中はハシゴの中央によって行く
            if (ControlDirectionY != 0)
            {
                if (Rectangle.X < ladder.Rectangle.X)
                {
                    Rectangle.X += 1;
                    Position.X = Rectangle.X;
                }
                else if (Rectangle.X > ladder.Rectangle.X)
                {
                    Rectangle.X -= 1;
                    Position.X = Rectangle.X;
                }
            }

            Velocity.Y = ControlDirectionY * AccelerationX;
            Velocity.Y = MathHelper.Clamp(Velocity.Y, -MaxSpeedX, MaxSpeedX);
            Rectangle.Y = (int)(Position.Y + Velocity.Y);

            foreach (GameObject o in Game2.PlaySc.NearMapObjs)
            {
                if (o.ObjectKind == GameObjectKinds.Cloud || o.ObjectKind == GameObjectKinds.Ladder || o.ObjectKind == GameObjectKinds.Disable)
                {
                    continue;
                }
                else if (Rectangle.Intersect(o.Rectangle, Rectangle).IsEmpty)
                {
                    continue;
                }

                //何かとぶつかったら、めり込まずに手前で止まる
                if (Velocity.Y < 0)
                {
                    Rectangle.Y = o.Rectangle.Bottom;
                    Velocity.Y = 0;
                }
                else if (Velocity.Y > 0)
                {
                    Rectangle.Y = o.Rectangle.Top - Height;
                    Velocity.Y = 0;
                }

                break;
            }

            //移動を確定する
            Position.Y = Rectangle.Y;
        }

        /// <summary>
        /// 重力のみ処理する
        /// </summary>
        public void OnlyGravity()
        {
            Velocity.Y += Gravity;
            Velocity.Y = MathHelper.Clamp(Velocity.Y, -MaxJumpSpeed, MaxJumpSpeed);
            Rectangle.Y = (int)(Position.Y + Velocity.Y);
            Position.Y = Rectangle.Y;

            if (Game2.PlaySc.IsOutOfMapY(Position.Y) && UseOutOfMapY)
            {
                OutOfMapY();
                ObjectStatus = PhysicsObjectStatus.Remove;
            }
        }

        /// <summary>
        /// 上下方向の物理制御をおこない、何かに接触したかを返す
        /// </summary>
        /// <returns>何かに接触したか</returns>
        public virtual bool JumpAndGravity()
        {
            //とりあえず落ちてみる
            Velocity.Y += Gravity;
            Velocity.Y = MathHelper.Clamp(Velocity.Y, -MaxJumpSpeed, MaxJumpSpeed);

            //先に接触判定だけ動かす
            Rectangle.Y = (int)(Position.Y + Velocity.Y);

            //接触の範囲
            GroundBlock = null;
            bool ret = false;
            bool first = true;
            GameObject temp = null;

            foreach (GameObject o in Game2.PlaySc.NearMapObjs)
            {
                if (o.ObjectKind == GameObjectKinds.Ladder || o.ObjectKind == GameObjectKinds.Disable)
                {
                    continue;
                }
                else if (Rectangle.Intersect(o.Rectangle, Rectangle).IsEmpty)
                {
                    continue;
                }

                //頭上の物体には一度だけ接触判定を行う
                if (first)
                {
                    first = false;
                    ret = true;

                    //上昇中、何かとぶつかったら、めり込まずに手前で止まる
                    if (Velocity.Y < 0)
                    {
                        //上昇中の雲は素通り
                        if (o.ObjectKind == GameObjectKinds.Cloud)
                        {
                            continue;
                        }

                        //頭ぶつけた
                        Rectangle.Y = o.Rectangle.Bottom;
                        Velocity.Y = 0;
                        break;
                    }
                }

                //下降中、以下の条件で雲を素通りする
                //プレーヤーがアイテムを持っており雲と上から接触したら乗れる
                //敵が雲と上から接触したら乗れる
                if (o.ObjectKind == GameObjectKinds.Cloud)
                {
                    if (Position.Y <= o.Rectangle.Top - Height)
                    {
                        if (ObjectKind == GameObjectKinds.Enemy || (ObjectKind == GameObjectKinds.Player && Game2.Session.Inventory.HasShoesItem()))
                        {
                            //素通りさせる
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

                if (temp == null)
                {
                    //着地
                    temp = o;
                }
                else
                {
                    //2オブジェクトにまたがった場合、多く接触した方の影響を受ける
                    //起点同士の距離が短い方が採用される
                    if (Math.Abs(Position.X - o.Position.X) < Math.Abs(Position.X - temp.Position.X))
                    {
                        temp = o;
                    }

                    break;
                }
            }

            if (temp != null)
            {
                //着地
                Rectangle.Y = temp.Rectangle.Top - Height;
                temp.AddConnection(this);
                Velocity.Y = 0;
            }

            //移動を確定する
            Position.Y = Rectangle.Y;

            if (Game2.PlaySc.IsOutOfMapY(Position.Y) && UseOutOfMapY)
            {
                OutOfMapY();
                ObjectStatus = PhysicsObjectStatus.Remove;
            }

            return ret;
        }

        /// <summary>
        /// 左右方向の物理制御をおこない、何かに接触したかを調べる
        /// </summary>
        /// <returns>接触の有無</returns>
        public virtual bool MoveLeftOrRight()
        {
            bool ret = false;
            Velocity.X += ControlDirectionX * (GroundBlock != null ? AccelerationX : AirAccelerationX);
            float f = 0;

            //ブロックからの抵抗を取得する
            if (GroundBlock != null)
            {
                f = GroundBlock.GetFriction(Velocity.X, ControlDirectionX);
            }
            else if (UseAirFriction)
            {
                f = GetAirFriction(Velocity.X);
            }

            //プルプルするのを止める
            if (Math.Sign(Velocity.X + f) != Math.Sign(Velocity.X))
            {
                Velocity.X = 0;
            }
            else
            {
                Velocity.X += f;
            }

            Velocity.X = MathHelper.Clamp(Velocity.X, -MaxSpeedX, MaxSpeedX);

            //物体との接触を行う
            //先に接触判定だけ動かす
            Rectangle.X = (int)(Position.X + Velocity.X);

            foreach (GameObject o in Game2.PlaySc.NearMapObjs)
            {
                if (o.ObjectKind == GameObjectKinds.Cloud || o.ObjectKind == GameObjectKinds.Ladder || o.ObjectKind == GameObjectKinds.Disable)
                {
                    continue;
                }
                else if (Rectangle.Intersect(o.Rectangle, Rectangle).IsEmpty)
                {
                    continue;
                }

                //何かとぶつかったら、めり込まずに手前で止まる
                if (Velocity.X < 0)
                {
                    Rectangle.X = o.Rectangle.Right;
                    Velocity.X = 0;
                }
                else if (Velocity.X > 0)
                {
                    Rectangle.X = o.Rectangle.Left - Width;
                    Velocity.X = 0;
                }

                ret = true;
                break;
            }

            //移動を確定する
            Position.X = Rectangle.X;
            return ret;
        }

        /// <summary>
        /// アニメーション画像を更新する
        /// </summary>
        public virtual void UpdateAnimation()
        {
            if (!UseAnimation)
            {
                return;
            }
            else if (ObjectStatus == PhysicsObjectStatus.Dead)
            {
                return;
            }
            else if (ObjectStatus == PhysicsObjectStatus.Damage)
            {
                return;
            }
            else if (OnLadder && LadderImg != null)
            {
                Img = LadderImg.GetImage(ControlDirectionY != 0);
            }
            else
            {
                if (ControlDirectionX < 0)
                {
                    Direction = -1;
                }
                else if (ControlDirectionX > 0)
                {
                    Direction = 1;
                }

                if (Direction < 0)
                {
                    Img = LImg.GetImage(ControlDirectionX != 0 && (AnimationAlways || GroundBlock != null));
                }
                else if (Direction > 0)
                {
                    Img = RImg.GetImage(ControlDirectionX != 0 && (AnimationAlways || GroundBlock != null));
                }
            }
        }

        /// <summary>
        /// ダメージ処理
        /// </summary>
        /// <param name="damage">ダメージ量</param>
        public virtual void Damage(int damage)
        {
            if (ObjectStatus == PhysicsObjectStatus.Dead)
            {
                return;
            }
            else if (ObjectStatus == PhysicsObjectStatus.Damage)
            {
                return;
            }

            DecLife(damage);
            Velocity = Vector2.Zero;

            if (Life <= 0)
            {
                Life = 0;
                ObjectStatus = PhysicsObjectStatus.Dead;
                Died();
            }
            else
            {
                ObjectStatus = PhysicsObjectStatus.Damage;
                DamageTimer.Start(DamageTime);
                Damaged();
            }
        }

        /// <summary>
        /// ダメージによるライフの減算処理
        /// </summary>
        /// <param name="damage">ダメージ量</param>
        public virtual void DecLife(int damage)
        {
            Life -= damage;
        }

        /// <summary>
        /// 画面から除去された時の処理
        /// </summary>
        public virtual void Removed()
        {

        }

        /// <summary>
        /// ダメージを受けた時の処理
        /// </summary>
        public virtual void Damaged()
        {
            Game2.MusicPlayer.PlaySE(DamageSE);

            if (DamageImg != null)
            {
                Img = DamageImg;
            }
        }

        /// <summary>
        /// 死んだときの処理
        /// </summary>
        public virtual void Died()
        {
            Game2.MusicPlayer.PlaySE(DeadSE);

            if (DeadImg != null)
            {
                Img = DeadImg;
            }
        }

        /// <summary>
        /// ジャンプする
        /// </summary>
        public virtual void Jump()
        {
            if (GroundBlock != null)
            {
                Velocity.Y = -JumpAcceleration;
            }
        }

        /// <summary>
        /// 画面外に出た場合の処理
        /// </summary>
        public virtual void OutOfMapY()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (ObjectStatus == PhysicsObjectStatus.Damage)
            {
                if (Img == null)
                {
                    return;
                }

                spriteBatch.Draw(Game2.Images, Position, Img, Color.Red);
            }
            else
            {
                base.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// ダメージ状態から回復する
        /// </summary>
        public void RecoveryDamage()
        {
            if (ObjectStatus == PhysicsObjectStatus.Damage && !DamageTimer.Update())
            {
                ObjectStatus = PhysicsObjectStatus.Normal;
            }
        }
    }
}
