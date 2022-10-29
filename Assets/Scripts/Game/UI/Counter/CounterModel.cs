using System;
using Game.Settings.Interfaces;
using Game.UI.Counter.Interfaces;

namespace Game.UI.Counter
{
    public class CounterModel : ICounterModel
    {
        public CounterModel(ILevelSettings settings)
        {
            _count = settings.CoinsCount;
        }
        
        public event Action<int> OnCountChanged = _ => { };

        private int _count;
        
        public void Decrement()
        {
            _count--;
            OnCountChanged.Invoke(_count);
        }
    }
}