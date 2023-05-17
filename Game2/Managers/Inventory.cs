namespace Game2.Managers
{
    /// <summary>
    /// アイテム管理
    /// </summary>
    public class Inventory
    {
        private bool _doubleScore = false;
        private bool _finder = false;
        private bool _shield = false;
        private bool _time = false;
        private bool _light = false;
        private bool _sword = false;
        private bool _shoes = false;
        private bool _tripleShot = false;
        private bool _highJump = false;

        public Inventory()
        {
        }

        public void SetItem(string name, bool flag)
        {
            switch (name)
            {
                case "DoubleScore":

                    _doubleScore = flag;
                    break;

                case "Finder":

                    _finder = flag;
                    break;

                case "Shield":

                    _shield = flag;
                    break;

                case "Time":

                    _time = flag;
                    break;

                case "Light":

                    _light = flag;
                    break;

                case "Sword":

                    _sword = flag;
                    break;

                case "Shoes":

                    _shoes = flag;
                    break;

                case "TripleShot":

                    _tripleShot = flag;
                    break;

                case "HighJump":

                    _highJump = flag;
                    break;
            }
        }

        public bool HasDoubleScoreItem()
        {
            return _doubleScore;
        }

        public bool HasFinderItem()
        {
            return _finder;
        }

        public bool HasShieldItem()
        {
            return _shield;
        }

        public bool HasTimeItem()
        {
            return _time;
        }

        public bool HasLightItem()
        {
            return _light;
        }

        public bool HasSwordItem()
        {
            return _sword;
        }

        public bool HasShoesItem()
        {
            return _shoes;
        }

        public bool HasTripleShotItem()
        {
            return _tripleShot;
        }

        public bool HasHighJumpItem()
        {
            return _highJump;
        }

        public void SetAllFlags(bool flag)
        {
            _doubleScore = flag;
            _finder = flag;
            _shield = flag;
            _time = flag;
            _light = flag;
            _sword = flag;
            _shoes = flag;
            _tripleShot = flag;
            _highJump = flag;
        }
    }
}
