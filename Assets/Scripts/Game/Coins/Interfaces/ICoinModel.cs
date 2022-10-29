using System;

namespace Game.Coins.Interfaces
{
    public interface ICoinModel
    {
        event Action OnCoinTaken;
        
        void Take();
    }
}