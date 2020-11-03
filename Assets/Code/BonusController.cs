namespace mzmeevskiy
{
    public class BonusController
    {
        private int _bonusesCount;

        public delegate void BonusesChangedAction(int newValue);
        public event BonusesChangedAction BonusesChanged;

        public BonusController()
        {
            _bonusesCount = 0;
        }

        public void OnBonusCaught()
        {
            _bonusesCount += 1;
            BonusesChanged?.Invoke(_bonusesCount);
        }
        
    }
}