using System;

namespace Game.Coins.Interfaces
{
    public interface ICoinView
    {
        event Action OnCoinTaken;
    }
}