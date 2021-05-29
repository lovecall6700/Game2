using Game2.Managers;
using Game2.Utilities;
using Microsoft.Xna.Framework;

namespace Game2.GameObjects
{
    /// <summary>
    /// 主人公
    /// </summary>
    internal class Player : PhysicsObject
    {
        /// <summary>
        /// 座った絵
        /// </summary>
        private readonly Rectangle? _sitDownImg;

        /// <summary>
        /// 後ろ向きに立っている絵
        /// </summary>
        private readonly Rectangle? _stayImg;

        /// <summary>
        /// 再スタート位置
        /// </summary>
        private Vector2 _restartPosition;

        /// <summary>
        /// しゃがんでいる
        /// </summary>
        private bool _sitDown;

        /// <summary>
        /// 扉に入るつもり
        /// </summary>
        private bool _enterDoor;

        /// <summary>
        /// 連射間隔
        /// </summary>
        private readonly Timer _bulletTimer = new Timer();

        /// <summary>
        /// ジャンプ開始高度
        /// </summary>
        private float _jumpStartHeight;

        /// <summary>
        /// 最低ジャンプ高度
        /// </summary>
        private readonly float _jumpMinHeight = 24f;

        /// <summary>
        /// 立っているときの高さ
        /// </summary>
        private readonly int _standHeight = 32;

        /// <summary>
        /// 座っているときの高さ
        /// </summary>
        private readonly int _sitDownHeight = 20;

        /// <summary>
        /// 最大ライフ
        /// </summary>
        internal static readonly int MaxLife = 3;

        /// <summary>
        /// ジャンプ可能フラグ
        /// </summary>
        private bool _canJump = true;

        internal Player(ref Game2 game2, float x, float y) : base(ref game2, x, y)
        {
            RImg.ClearAndAddImage(Game2.Textures.GetTexture("PlayerR1"));
            RImg.AddImage(Game2.Textures.GetTexture("PlayerR2"));
            LImg.ClearAndAddImage(Game2.Textures.GetTexture("PlayerL1"));
            LImg.AddImage(Game2.Textures.GetTexture("PlayerL2"));
            LadderImg.ClearAndAddImage(Game2.Textures.GetTexture("PlayerLadder1"));
            LadderImg.AddImage(Game2.Textures.GetTexture("PlayerLadder2"));
            _sitDownImg = Game2.Textures.GetTexture("PlayerSit");
            DeadImg = Game2.Textures.GetTexture("PlayerDead");
            _stayImg = Game2.Textures.GetTexture("PlayerStay");
            DeadSE = "SoundEffects/PlayerDead";
            DamageSE = "SoundEffects/PlayerDead";
            UseAirFriction = true;
            UseLadder = true;
            ObjectKind = GameObjectKinds.Player;
            _restartPosition = Position;
            SetSize(16, 32);
            Restart();
        }

        /// <summary>
        /// 再スタート時の状態リセット
        /// </summary>
        internal override void Restart()
        {
            Life = Game2.Session.Life;
            Position = _restartPosition;
            Img = _stayImg;
            Velocity = Vector2.Zero;
            Direction = 0;
            ControlDirectionX = 0;
            _bulletTimer.Running = false;
            ObjectStatus = PhysicsObjectStatus.Normal;
            DamageTimer.Running = false;
            _enterDoor = false;
            StandUp();
        }

        internal override void Update(ref GameTime gameTime)
        {
            if (ObjectStatus == PhysicsObjectStatus.Dead)
            {
                OnlyGravity();
            }
            else
            {
                MoveLeftOrRight(ref gameTime);

                if (UseLadder)
                {
                    Ladder(ref gameTime);
                }
                else
                {
                    JumpAndGravity(ref gameTime);
                }
            }

            RecoveryDamage(ref gameTime);

            if (ObjectStatus == PhysicsObjectStatus.Normal)
            {
                if (Game2.GameCtrl.Left)
                {
                    ControlDirectionX = -1;
                }
                else if (Game2.GameCtrl.Right)
                {
                    ControlDirectionX = 1;
                }
                else
                {
                    ControlDirectionX = 0;
                }

                if (Game2.GameCtrl.Up)
                {
                    StandUp();

                    if (OnLadder)
                    {
                        ControlDirectionY = -1;
                        _enterDoor = false;
                    }
                    else
                    {
                        ControlDirectionY = 0;
                        _enterDoor = true;
                    }
                }
                else if (Game2.GameCtrl.Down)
                {
                    _enterDoor = false;

                    if (OnLadder)
                    {
                        ControlDirectionY = 1;
                    }
                    else
                    {
                        ControlDirectionY = 0;

                        if (Direction != 0 && GroundBlock != null)
                        {
                            SitDown();
                            Velocity.X /= 2f;
                            ControlDirectionX = 0;
                        }
                    }
                }
                else
                {
                    StandUp();
                    _enterDoor = false;
                    ControlDirectionY = 0;
                }

                //発砲
                if (!_bulletTimer.Update(ref gameTime) && !OnLadder && Game2.GameCtrl.Fire)
                {
                    StandUp();

                    //左右を向いていないときは発砲しない
                    if (Direction == -1)
                    {
                        Game2.PlaySc.PhysicsObjs.Add(new PlayerBullet(ref Game2, Position.X, Position.Y + 8, Direction));
                        Game2.MusicPlayer.PlaySE("SoundEffects/PlayerShot");
                        _bulletTimer.Start(200f, true);

                        if (Game2.Inventory.HasTripleShotItem())
                        {
                            Game2.PlaySc.PhysicsObjs.Add(new PlayerBullet(ref Game2, Position.X, Position.Y, Direction));
                            Game2.PlaySc.PhysicsObjs.Add(new PlayerBullet(ref Game2, Position.X, Position.Y + 16, Direction));
                        }
                    }
                    else if (Direction == 1)
                    {
                        Game2.PlaySc.PhysicsObjs.Add(new PlayerBullet(ref Game2, Position.X + 8, Position.Y + 8, Direction));
                        Game2.MusicPlayer.PlaySE("SoundEffects/PlayerShot");
                        _bulletTimer.Start(200f, true);

                        if (Game2.Inventory.HasTripleShotItem())
                        {
                            Game2.PlaySc.PhysicsObjs.Add(new PlayerBullet(ref Game2, Position.X + 8, Position.Y, Direction));
                            Game2.PlaySc.PhysicsObjs.Add(new PlayerBullet(ref Game2, Position.X + 8, Position.Y + 16, Direction));
                        }
                    }
                }

                if (!_canJump && !Game2.GameCtrl.Jump)
                {
                    _canJump = true;
                }

                if (_canJump && GroundBlock != null && Game2.GameCtrl.Jump && Direction != 0)
                {
                    StandUp();
                    _jumpStartHeight = Position.Y;
                    Jump();
                    _canJump = false;
                }
                else if (GroundBlock == null && Velocity.Y < 0 && !Game2.GameCtrl.Jump && _jumpStartHeight - Position.Y > _jumpMinHeight)
                {
                    //ちょいジャンプ対応、ジャンプ中にジャンプボタンを離したら落下する
                    //最低ジャンプ高度に達していない場合はジャンプを続ける
                    Velocity.Y = 0;
                }
            }
            else if (ObjectStatus == PhysicsObjectStatus.Dead)
            {
                //死亡時は扉に逃げ込むことができ、扉に入ればミスにならない
                _enterDoor = true;
            }

            if (!_sitDown)
            {
                UpdateAnimation();
            }

            foreach (GameObject o in Game2.PlaySc.ItemObjs)
            {
                if (!Rectangle.Intersect(o.Rectangle, Rectangle).IsEmpty)
                {
                    CollisionWithItem(o);
                }
            }
        }

        internal override void Jump()
        {
            if (Game2.Inventory.HasHighJumpItem())
            {
                Gravity = 0.5f;
            }
            else
            {
                Gravity = 1;
            }

            base.Jump();
        }

        internal override void Died()
        {
            StandUp();
            base.Died();
            Game2.Inventory.SetItem("TripleShot", false);
            Game2.Inventory.SetItem("Sword", false);
            Game2.Inventory.SetItem("Finder", false);
        }

        private void SitDown()
        {
            if (_sitDown)
            {
                return;
            }

            _sitDown = true;
            Height = _sitDownHeight;
            Rectangle.Height = _sitDownHeight;
            Rectangle.Offset(0, _standHeight - _sitDownHeight);
            Position.Y += _standHeight - _sitDownHeight;
            Img = _sitDownImg;
        }

        private void StandUp()
        {
            if (!_sitDown)
            {
                return;
            }

            _sitDown = false;
            Height = _standHeight;
            Rectangle.Height = _standHeight;
            Rectangle.Offset(0, _sitDownHeight - _standHeight);
            Position.Y += _sitDownHeight - _standHeight;
        }

        private string GetDblScoreSymbol()
        {
            if (Game2.Inventory.HasDoubleScoreItem())
            {
                return "2x";
            }

            return "";
        }

        private void CollisionWithItem(GameObject item)
        {
            switch (item.ObjectKind)
            {
                case GameObjectKinds.Item:

                    Item i = (Item)item;

                    if (i.Visibility == ObjectVisibility.Normal)
                    {
                        i.Visibility = ObjectVisibility.Disable;
                        Game2.Inventory.SetItem(i.Name, true);
                        Game2.Session.AddItem(i);
                        Game2.MusicPlayer.PlaySE("SoundEffects/GetItem");
                        Life = MaxLife;
                    }
                    else if (i.Visibility == ObjectVisibility.Hidden && (_sitDown || Game2.Inventory.HasFinderItem()))
                    {
                        i.Visibility = ObjectVisibility.Normal;
                        int s = Game2.Session.StageNo * Game2.FindBonus;
                        Game2.AddScore(s);
                        Game2.PlaySc.EffectObjs.Add(new PopupMessage(ref Game2, ref Game2.Font, Position.X, Position.Y, GetDblScoreSymbol() + s.ToString()));
                        Game2.Session.AddItem(i);
                        Game2.MusicPlayer.PlaySE("SoundEffects/Find");
                    }

                    break;

                case GameObjectKinds.Door:

                    Door d = (Door)item;

                    if (d.Visibility == ObjectVisibility.Normal && _enterDoor || d.Visibility == ObjectVisibility.Invisible)
                    {
                        Game2.Session.DestStageNo = d.DestStageNo;
                        Game2.Session.DestDoorNo = d.DestDoorNo;

                        if (Life <= 0)
                        {
                            Life = 1;
                        }

                        Game2.Session.Life = Life;
                        Game2.Scheduler.SetSchedule(Schedules.EnterDoor);

                    }
                    else if (d.Visibility == ObjectVisibility.Hidden && (_sitDown || Game2.Inventory.HasFinderItem()))
                    {
                        d.Visibility = ObjectVisibility.Normal;
                        int s = Game2.Session.StageNo * Game2.FindBonus;
                        Game2.AddScore(s);
                        Game2.PlaySc.EffectObjs.Add(new PopupMessage(ref Game2, ref Game2.Font, Position.X, Position.Y, GetDblScoreSymbol() + s.ToString()));
                        Game2.Session.AddDoor(d);
                        Game2.MusicPlayer.PlaySE("SoundEffects/Find");
                    }

                    break;

                case GameObjectKinds.TreasureBox:

                    TreasureBox t = (TreasureBox)item;

                    if (t.Visibility == ObjectVisibility.Normal)
                    {
                        t.Visibility = ObjectVisibility.Open;
                        Game2.AddScore(t.Score);
                        Game2.PlaySc.EffectObjs.Add(new PopupMessage(ref Game2, ref Game2.Font, Position.X, Position.Y, GetDblScoreSymbol() + t.Score.ToString()));
                        Game2.Session.AddTreasureBox(t);
                        Game2.MusicPlayer.PlaySE("SoundEffects/GetItem");
                        Life = MaxLife;
                    }
                    else if (t.Visibility == ObjectVisibility.Hidden && (_sitDown || Game2.Inventory.HasFinderItem()))
                    {
                        t.Visibility = ObjectVisibility.Normal;
                        Game2.Session.AddTreasureBox(t);
                        Game2.MusicPlayer.PlaySE("SoundEffects/Find");
                    }

                    break;
            }
        }

        internal override void Removed()
        {
            Game2.Scheduler.SetSchedule(Schedules.RestartOrGameover);
        }

        internal override void OutOfMapY()
        {
            if (ObjectStatus != PhysicsObjectStatus.Dead)
            {
                //死んでいないのに画面外に出たなら落下死
                Game2.Inventory.SetItem("Shield", false);
                Game2.Inventory.SetItem("HighJump", false);
            }
        }

        internal override void DecLife(int damage)
        {
            if (!DamageTimer.Running)
            {
                base.DecLife(damage);
                Game2.Inventory.SetItem("DoubleScore", false);
                Game2.Inventory.SetItem("Sword", false);
            }
        }

        internal override void Damaged()
        {
            base.Damaged();
            Velocity = new Vector2(-10 * Direction, -5);
        }
    }
}
