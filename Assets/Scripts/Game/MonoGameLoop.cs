using Game.Interfaces;
using Game.UI.Timer;
using UnityEngine;
using Utils.Signals;
using Utils.Signals.Interfaces;
using Zenject;

namespace Game
{
    public class MonoGameLoop : MonoBehaviour, IGameLoop
    {
        public ISignal LevelLoaded
        {
            get;
        } = new Signal();

        public ISignal TimerOver
        {
            get;
        } = new Signal();

        public ISignal GameOver
        {
            get;
        } = new Signal();

        private void Start()
        {
            LevelLoaded.Invoke();
        }
    }
}