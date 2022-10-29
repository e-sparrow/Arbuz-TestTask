using System;
using Game.UI.Timer.Interfaces;

namespace Game.UI.Timer
{
    public class TimerModel : ITimerModel
    {
        public event Action<float> OnTimerStarted = _ => { };
        
        public void StartTimer(float time)
        {
            OnTimerStarted.Invoke(time);
        }
    }
}