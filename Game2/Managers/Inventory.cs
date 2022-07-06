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
        private readonly Game2 _game2;

        public Inventory(Game2 game2)
        {
            _game2 = game2;
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

        private void FalseAllItems()
        {
            _doubleScore = false;
            _finder = false;
            _shield = false;
            _time = false;
            _light = false;
            _sword = false;
            _shoes = false;
            _tripleShot = false;
            _highJump = false;

            //裏技
            if (_game2.Session.InfiniteItem)
            {
                _doubleScore = true;
                _finder = true;
                _shield = true;
                _time = true;
                _light = true;
                _sword = true;
                _shoes = true;
                _tripleShot = true;
                _highJump = true;
            }
        }

        public void GameoverRetryToStart()
        {
            FalseAllItems();
        }

        public void TitleToInitialStart()
        {
            FalseAllItems();
        }

        public void TitleToLoadStart()
        {
            FalseAllItems();
        }
    }
}
