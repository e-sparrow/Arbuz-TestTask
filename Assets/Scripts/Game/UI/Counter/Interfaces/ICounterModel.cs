using System;

namespace Game.UI.Counter.Interfaces
{
    public interface ICounterModel
    {
        event Action<int> OnCountChanged; 

        void Decrement();
    }
}