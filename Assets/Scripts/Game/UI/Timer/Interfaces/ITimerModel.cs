using System;

namespace Game.UI.Timer.Interfaces
{
    public interface ITimerModel
    {
        event Action<float> OnTimerStarted;

        void StartTimer(float time);
    }
}