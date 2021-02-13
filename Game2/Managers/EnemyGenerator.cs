using Game2.GameObjects;
using Game2.Utilities;
using Microsoft.Xna.Framework;
using System;

namespace Game2.Managers
{
    /// <summary>
    /// 敵を生成する
    /// </summary>
    internal class EnemyGenerator
    {
        private readonly Random _rnd = new Random();
        private readonly Game2 _game2;

        /// <summary>
        /// 敵を生成するタイマー
        /// </summary>
        private readonly Timer _timer = new Timer();

        /// <summary>
        /// 敵を生成する間隔
        /// </summary>
        private static readonly float spawnInterval = 2000f;

        /// <summary>
        /// 出てくる予定の敵
        /// </summary>
        private readonly bool[] _enemies = new bool[11];

        /// <summary>
        /// 出現する敵の種類
        /// </summary>
        private int _counter = 0;

        /// <summary>
        /// ボスは1プレイ1回しか出てこない
        /// </summary>
        internal bool BossFlag = false;

        internal EnemyGenerator(Game2 game2)
        {
            _game2 = game2;
        }

        internal void Update(ref Vector2 offset, ref GameTime gameTime)
        {
            //画面に敵が多すぎる場合は敵を出さない
            if (_game2.PlaySc.PhysicsObjs.Count > 30)
            {
                return;
            }

            //敵の出現は一定間隔
            if (_timer.Update(ref gameTime))
            {
                return;
            }

            _counter = (_counter + 1) % _enemies.Length;

            if (!_enemies[_counter])
            {
                return;
            }

            Enemy e = GetEnemy();

            if (e == null)
            {
                return;
            }

            if (_counter == 1 || _counter == 5)
            {
                //1と5は飛行タイプなので地形無視で左右から出現する
                SetEnemyPositionFlying(ref offset, ref e);
            }
            else if (_counter == 4)
            {
                //4はプクプクタイプ
                SetEnemyPositionJumping(ref offset, ref e);
            }
            else if (_counter == 10 && e != null)
            {
                if (_game2.Session.StageNo == 24)
                {
                    e.Position = new Vector2(136, -80);
                }
                else
                {
                    e.Position = new Vector2(272, -128);
                }

                e.Rectangle.Location = e.Position.ToPoint();
            }
            else if (_game2.PlaySc.StageDir == Screens.StageDirType.Horizontal)
            {
                SetEnemyPositionLeftRight(ref offset, ref e);
            }
            else
            {
                SetEnemyPositionUp(ref offset, ref e);
            }

            if (e != null)
            {
                _game2.PlaySc.PhysicsObjs.Add(e);
                _timer.Start(spawnInterval, true);
            }
        }

        private void SetEnemyPositionFlying(ref Vector2 offset, ref Enemy e)
        {
            int y = _rnd.Next(0, Game2.Height - e.Height);

            if (_rnd.Next(0, 2) == 0)
            {
                e.Position = new Vector2(-e.Width, y) + offset;
                e.ControlDirectionX = 1;
            }
            else
            {
                e.Position = new Vector2(256, y) + offset;
                e.ControlDirectionX = -1;
            }

            e.Rectangle.Location = e.Position.ToPoint();
        }

        private void SetEnemyPositionLeftRight(ref Vector2 offset, ref Enemy e)
        {
            int r;
            Player p = _game2.PlaySc.Player;

            if (p.Direction == 0)
            {
                r = _rnd.Next(0, 2);
            }
            else
            {
                r = _rnd.Next(0, 6);
            }

            if (r == 0 || (r > 1 && p.Direction == -1))
            {
                e.Rectangle.X = (int)(offset.X - e.Width);
                e.ControlDirectionX = 1;
            }
            else if (r == 1 || (r > 1 && p.Direction == 1))
            {
                e.Rectangle.X = (int)(offset.X + 256);
                e.ControlDirectionX = -1;
            }

            e.Rectangle.Y = _rnd.Next(0, 256 - e.Height);

            foreach (GameObject o in _game2.PlaySc.NearMapObjs)
            {
                if (!Rectangle.Intersect(o.Rectangle, e.Rectangle).IsEmpty)
                {
                    e = null;
                    return;
                }
            }

            for (int i = e.Rectangle.Y; i < 256 - e.Height; i++)
            {
                e.Rectangle.Y = i;

                foreach (GameObject o in _game2.PlaySc.NearMapObjs)
                {
                    if (!Rectangle.Intersect(o.Rectangle, e.Rectangle).IsEmpty)
                    {
                        e.Rectangle.Y = i - 1;
                        e.Position = e.Rectangle.Location.ToVector2();
                        return;
                    }
                }
            }

            e = null;
        }

        private void SetEnemyPositionJumping(ref Vector2 offset, ref Enemy e)
        {
            e.Position = new Vector2(_rnd.Next(48, 208), 256) + offset;
            e.Rectangle.Location = e.Position.ToPoint();
            e.Jump();

            if (e.Position.X < 128)
            {
                e.ControlDirectionX = 1;
            }
            else
            {
                e.ControlDirectionX = -1;
            }
        }

        private void SetEnemyPositionUp(ref Vector2 offset, ref Enemy e)
        {
            e.Rectangle.X = _rnd.Next(0, 256 - e.Width);
            e.Rectangle.Y = (int)(offset.Y - e.Height);

            foreach (GameObject o in _game2.PlaySc.NearMapObjs)
            {
                if (!Rectangle.Intersect(o.Rectangle, e.Rectangle).IsEmpty)
                {
                    e = null;
                    return;
                }
            }

            if (e.Position.X < 128)
            {
                e.ControlDirectionX = 1;
            }
            else
            {
                e.ControlDirectionX = -1;
            }

            e.Position = e.Rectangle.Location.ToVector2();
        }


        private Enemy GetEnemy()
        {
            /// 敵0(直進、壁で反転、穴に落下): EnemyDog
            /// 敵1(直進、飛行): EnemyBird
            /// 敵2(動かない): EnemyNeedle
            /// 敵3(直進、ジャンプ、壁で反転、穴に落下): EnemyDogJump
            /// 敵4(下からジャンプして出現、壁透過、放物線): EnemyUPeopleJump
            /// 敵5(飛行、追尾): EnemyBirdHoming
            /// 敵6(敵1、前面の攻撃を防御): EnemyUPeopleShield
            /// 敵7(敵4、プレーヤーを左右攻撃): EnemyUPeopleNormal
            /// 敵8(敵4、プレーヤーを八方攻撃): EnemyUPeopleFire
            /// 敵9(敵7、プレーヤーを左右攻撃): EnemyUPeopleFighter
            /// ボス: EnemyBoss
            switch (_counter)
            {
                case 0:

                    return new EnemyDog(_game2, 0, 0);

                case 1:

                    return new EnemyBird(_game2, 0, 0);

                case 2:

                    return new EnemyNeedle(_game2, 0, 0);

                case 3:

                    return new EnemyJumpDog(_game2, 0, 0);

                case 4:

                    return new EnemyJumpFish(_game2, 0, 0);

                case 5:

                    return new EnemyHomingFire(_game2, 0, 0);

                case 6:

                    return new EnemyUPeopleBasic(_game2, 0, 0);

                case 7:

                    return new EnemyUPeopleJump(_game2, 0, 0);

                case 8:

                    return new EnemyUPeople8Shots(_game2, 0, 0);

                case 9:

                    return new EnemyUPeopleLife(_game2, 0, 0);

                case 10:

                    if (BossFlag)
                    {
                        return null;
                    }

                    BossFlag = true;

                    // UFOルートのステージは特殊UFO戦
                    if (_game2.Session.StageNo == 24)
                    {
                        return new EnemyUFO2(_game2, 0, 0);
                    }

                    return new EnemyUFO(_game2, 0, 0);
            }

            return null;
        }

        /// <summary>
        /// 出現する敵のフラグ操作
        /// </summary>
        /// <param name="enemyNo">敵番号</param>
        /// <param name="flag">敵が出現予定か</param>
        internal void SetSpawn(int enemyNo, bool flag)
        {
            _enemies[enemyNo] = flag;
        }
    }
}
