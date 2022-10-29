using Game.Level.Interfaces;
using Game.UI.Timer;
using Game.UI.Timer.Interfaces;
using UnityEngine;

namespace Game.Level
{
    public class LevelView : MonoBehaviour, ILevelView
    {
        [SerializeField] private TimerViewWithPanel loadingTimer;
        [SerializeField] private TimerView inGameTimer;

        public ITimerView LoadingTimer => loadingTimer;
        public ITimerView InGameTimer => inGameTimer;
    }
}