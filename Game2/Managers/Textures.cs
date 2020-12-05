using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Game2.Managers
{
    /// <summary>
    /// テクスチャ管理
    /// </summary>
    internal class Textures
    {
        /// <summary>
        /// テクスチャ名でテクスチャを管理
        /// </summary>
        private readonly Dictionary<string, Rectangle> _rectangles = new Dictionary<string, Rectangle>();

        public Textures()
        {
            _rectangles.Add("BeltConveyer", new Rectangle(0, 80, 16, 16));
            _rectangles.Add("Brick", new Rectangle(0, 64, 16, 16));
            _rectangles.Add("Bridge", new Rectangle(16, 64, 16, 16));
            _rectangles.Add("Cloud", new Rectangle(272, 64, 32, 16));
            _rectangles.Add("Crack1", new Rectangle(16, 80, 16, 16));
            _rectangles.Add("Crack2", new Rectangle(32, 80, 16, 16));
            _rectangles.Add("Crack3", new Rectangle(48, 80, 16, 16));
            _rectangles.Add("DarkRock", new Rectangle(32, 64, 16, 16));
            _rectangles.Add("DoorClose", new Rectangle(224, 0, 16, 32));
            _rectangles.Add("End1", new Rectangle(0, 96, 256, 128));
            _rectangles.Add("End2", new Rectangle(256, 96, 256, 128));
            _rectangles.Add("End3", new Rectangle(512, 96, 256, 128));
            _rectangles.Add("End4", new Rectangle(768, 96, 256, 128));
            _rectangles.Add("End5", new Rectangle(0, 224, 256, 128));
            _rectangles.Add("EnemyBirdDead", new Rectangle(112, 32, 16, 16));
            _rectangles.Add("EnemyBirdL1", new Rectangle(80, 32, 16, 16));
            _rectangles.Add("EnemyBirdL2", new Rectangle(80, 48, 16, 16));
            _rectangles.Add("EnemyBirdR1", new Rectangle(96, 32, 16, 16));
            _rectangles.Add("EnemyBirdR2", new Rectangle(96, 48, 16, 16));
            _rectangles.Add("EnemyBossBody", new Rectangle(112, 48, 16, 16));
            _rectangles.Add("EnemyBossL1", new Rectangle(128, 32, 16, 16));
            _rectangles.Add("EnemyBossR1", new Rectangle(128, 48, 16, 16));
            _rectangles.Add("EnemyBullet", new Rectangle(272, 56, 8, 8));
            _rectangles.Add("EnemyDogDead", new Rectangle(176, 32, 16, 16));
            _rectangles.Add("EnemyDogL1", new Rectangle(144, 32, 16, 16));
            _rectangles.Add("EnemyDogL2", new Rectangle(144, 48, 16, 16));
            _rectangles.Add("EnemyDogR1", new Rectangle(160, 32, 16, 16));
            _rectangles.Add("EnemyDogR2", new Rectangle(160, 48, 16, 16));
            _rectangles.Add("EnemyFireDead", new Rectangle(256, 32, 16, 16));
            _rectangles.Add("EnemyFireL1", new Rectangle(224, 32, 16, 16));
            _rectangles.Add("EnemyFireL2", new Rectangle(224, 48, 16, 16));
            _rectangles.Add("EnemyFireR1", new Rectangle(240, 32, 16, 16));
            _rectangles.Add("EnemyFireR2", new Rectangle(240, 48, 16, 16));
            _rectangles.Add("EnemyJumpFishDead", new Rectangle(176, 48, 16, 16));
            _rectangles.Add("EnemyJumpFishL1", new Rectangle(192, 32, 16, 16));
            _rectangles.Add("EnemyJumpFishL2", new Rectangle(192, 48, 16, 16));
            _rectangles.Add("EnemyJumpFishR1", new Rectangle(208, 32, 16, 16));
            _rectangles.Add("EnemyJumpFishR2", new Rectangle(208, 48, 16, 16));
            _rectangles.Add("EnemyNeedle", new Rectangle(256, 48, 16, 16));
            _rectangles.Add("EnemyUFO", new Rectangle(768, 224, 256, 128));
            _rectangles.Add("EnemyUPeopleDead", new Rectangle(64, 32, 16, 32));
            _rectangles.Add("EnemyUPeopleL1", new Rectangle(0, 32, 16, 32));
            _rectangles.Add("EnemyUPeopleL2", new Rectangle(16, 32, 16, 32));
            _rectangles.Add("EnemyUPeopleR1", new Rectangle(32, 32, 16, 32));
            _rectangles.Add("EnemyUPeopleR2", new Rectangle(48, 32, 16, 32));
            _rectangles.Add("Flower", new Rectangle(48, 64, 16, 16));
            _rectangles.Add("Grass", new Rectangle(64, 64, 16, 16));
            _rectangles.Add("Ice", new Rectangle(80, 64, 16, 16));
            _rectangles.Add("ItemDoubleScore", new Rectangle(144, 0, 16, 16));
            _rectangles.Add("ItemFinder", new Rectangle(144, 16, 16, 16));
            _rectangles.Add("ItemHighJump", new Rectangle(160, 0, 16, 16));
            _rectangles.Add("ItemLight", new Rectangle(160, 16, 16, 16));
            _rectangles.Add("ItemShield", new Rectangle(176, 0, 16, 16));
            _rectangles.Add("ItemShoes", new Rectangle(176, 16, 16, 16));
            _rectangles.Add("ItemSword", new Rectangle(192, 0, 16, 16));
            _rectangles.Add("ItemTime", new Rectangle(192, 16, 16, 16));
            _rectangles.Add("ItemTripleShot", new Rectangle(208, 0, 16, 16));
            _rectangles.Add("Ladder", new Rectangle(96, 64, 16, 16));
            _rectangles.Add("MovingFloor", new Rectangle(64, 80, 48, 10));
            _rectangles.Add("Null", new Rectangle(208, 16, 16, 16));
            _rectangles.Add("Pillar", new Rectangle(128, 64, 16, 16));
            _rectangles.Add("PillarHead", new Rectangle(144, 64, 16, 16));
            _rectangles.Add("PillarRoot", new Rectangle(160, 64, 16, 16));
            _rectangles.Add("PlayerBulletL", new Rectangle(128, 20, 8, 8));
            _rectangles.Add("PlayerBulletR", new Rectangle(136, 20, 8, 8));
            _rectangles.Add("PlayerDead", new Rectangle(112, 0, 16, 32));
            _rectangles.Add("PlayerL1", new Rectangle(0, 0, 16, 32));
            _rectangles.Add("PlayerL2", new Rectangle(16, 0, 16, 32));
            _rectangles.Add("PlayerLadder1", new Rectangle(64, 0, 16, 32));
            _rectangles.Add("PlayerLadder2", new Rectangle(80, 0, 16, 32));
            _rectangles.Add("PlayerR1", new Rectangle(32, 0, 16, 32));
            _rectangles.Add("PlayerR2", new Rectangle(48, 0, 16, 32));
            _rectangles.Add("PlayerSit", new Rectangle(128, 0, 16, 20));
            _rectangles.Add("PlayerStay", new Rectangle(96, 0, 16, 32));
            _rectangles.Add("Reed", new Rectangle(176, 64, 16, 16));
            _rectangles.Add("ReedHead", new Rectangle(192, 64, 16, 16));
            _rectangles.Add("Rock", new Rectangle(208, 64, 16, 16));
            _rectangles.Add("Sand", new Rectangle(224, 64, 16, 16));
            _rectangles.Add("Story1", new Rectangle(256, 224, 256, 128));
            _rectangles.Add("Story2", new Rectangle(512, 224, 256, 128));
            _rectangles.Add("Story3", new Rectangle(0, 352, 256, 128));
            _rectangles.Add("Story4", new Rectangle(256, 352, 256, 128));
            _rectangles.Add("Title", new Rectangle(512, 352, 256, 128));
            _rectangles.Add("TreasureBoxClose", new Rectangle(240, 64, 16, 16));
            _rectangles.Add("TreasureBoxOpen", new Rectangle(256, 64, 16, 16));
            _rectangles.Add("Water", new Rectangle(112, 64, 16, 16));
        }

        /// <summary>
        /// テクスチャを取得する
        /// </summary>
        /// <param name="name">テクスチャ名</param>
        /// <returns>テクスチャ</returns>
        internal Rectangle? GetTexture(string name)
        {
            if (_rectangles.ContainsKey(name))
            {
                return _rectangles[name];
            }

            return null;
        }
    }
}
