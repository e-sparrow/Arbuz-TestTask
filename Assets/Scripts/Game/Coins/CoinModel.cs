using System;
using Game.Coins.Interfaces;

namespace Game.Coins
{
    public class CoinModel : ICoinModel
    {
        public event Action OnCoinTaken = () => { };

        public void Take()
        {
            OnCoinTaken.Invoke();
        }
    }
}