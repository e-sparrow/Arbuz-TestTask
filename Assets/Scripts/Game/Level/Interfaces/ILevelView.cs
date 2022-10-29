using Game.UI.Timer.Interfaces;

namespace Game.Level.Interfaces
{
    public interface ILevelView
    {
        ITimerView LoadingTimer
        {
            get;
        }
        
        ITimerView InGameTimer
        {
            get;
        }
    }
}