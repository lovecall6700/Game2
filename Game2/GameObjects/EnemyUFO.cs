using Microsoft.Xna.Framework;
using System;

namespace Game2.GameObjects
{
    public class EnemyUFO : Enemy
    {
        private int _state = 0;

        public EnemyUFO(Game2 game2, float x, float y) : base(game2, x, y)
        {
            Img = Game2.Textures.GetTexture("EnemyUFO");
            SetSize(256, 80);
            Attack = 255;
        }

        public override void Update(GameTime gameTime)
        {
            AttackPlayer();

            if (_state == 0)
            {
                Position.Y++;

                if (Position.Y > 20)
                {
                    _state = 1;
                    CreateBoss();
                }
            }
            else if (_state == 1)
            {
                Position.Y--;

                if (Position.Y < -128)
                {
                    ObjectStatus = PhysicsObjectStatus.Remove;
                }
            }

            Rectangle.Location = Position.ToPoint();
        }

        /// <summary>
        /// ボスを発生させる
        /// </summary>
        private void CreateBoss()
        {
            //ボス
            int max = 8;
            EnemyBoss[] body = new EnemyBoss[max];

            for (int i = 0; i < max; i++)
            {
                body[i] = new EnemyBoss(Game2, 0, 0, i);

                if (i > 0)
                {
                    body[i].RootBody = body[0];
                    body[i].UpperBody = body[i - 1];
                }
            }

            body[max - 1].Tail = true;
            body[0].Position.X = Position.X + 120;
            body[0].Position.Y = Position.Y + 110;
            body[0].ResetAllHistory();
            Array.Reverse(body);

            foreach (EnemyBoss item in body)
            {
                Game2.PlaySc.PhysicsObjs.Add(item);
            }
        }

        public override void Damage(int damage)
        {
            //無敵
        }
    }
}