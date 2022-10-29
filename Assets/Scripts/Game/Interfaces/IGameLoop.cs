using Utils.Signals.Interfaces;

namespace Game.Interfaces
{
    public interface IGameLoop
    {
        ISignal LevelLoaded
        {
            get;
        }

        ISignal TimerOver
        {
            get;
        }

        ISignal GameOver
        {
            get;
        }
    }
}