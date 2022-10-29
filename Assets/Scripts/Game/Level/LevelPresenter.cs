using System.Threading;
using Game.Interfaces;
using Game.Level.Interfaces;
using Game.Services.Sound.Enums;
using Game.Services.Sound.Interfaces;
using Game.Settings.Interfaces;
using Game.UI.Timer;
using Game.UI.Timer.Interfaces;
using Input;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Level
{
    public class LevelPresenter : IInitializable, ILateDisposable
    {
        public LevelPresenter(ILevelView view, ILevelSettings settings, ISoundService<ESoundType> soundService, IGameLoop gameLoop)
        {
            _view = view;
            _settings = settings;

            _soundService = soundService;
            _gameLoop = gameLoop;
        }

        private const int SceneIndex = 0;

        private readonly ILevelView _view;
        private readonly ILevelSettings _settings;

        private readonly ISoundService<ESoundType> _soundService;
        private readonly IGameLoop _gameLoop;

        private ITimerModel _loadingTimerModel;
        private ITimerModel _inGameTimerModel;

        private TimerPresenter _loadingTimerPresenter;
        private TimerPresenter _inGameTimerPresenter;

        private Controls _controls;
        
        public void Initialize()
        {
            _controls = new Controls();
            _controls.Enable();
            
            _loadingTimerModel = new TimerModel();
            _loadingTimerPresenter = new TimerPresenter(_loadingTimerModel, _view.LoadingTimer, _soundService);
            _view.LoadingTimer.SetValue(_settings.LoadingTimerLength);
            
            _loadingTimerPresenter.Initialize();
            _controls.Main.Click.performed += StartLoadingTimer;
            
            _inGameTimerModel = new TimerModel();
            _inGameTimerPresenter = new TimerPresenter(_inGameTimerModel, _view.InGameTimer, _soundService);
            _view.InGameTimer.SetValue(_settings.InGameTimerLength);
            
            _inGameTimerPresenter.Initialize();
            _gameLoop.TimerOver.OnInvoke += StartInGameTimer;
            
            _gameLoop.GameOver.OnInvoke += GameOver;
        }

        public void LateDispose()
        {
            _loadingTimerPresenter.LateDispose();   
            
            _inGameTimerPresenter.LateDispose();
            _gameLoop.TimerOver.OnInvoke -= StartInGameTimer;
            
            _gameLoop.GameOver.OnInvoke -= GameOver;
        }

        private void GameOver()
        {
            SceneManager.LoadScene(SceneIndex);
        }
        
        private void StartLoadingTimer(InputAction.CallbackContext context)
        {
            _controls.Main.Click.performed -= StartLoadingTimer;
            
            _loadingTimerModel.StartTimer(_settings.LoadingTimerLength);
            
            _loadingTimerPresenter.OnTimerOver += InvokeTimerOver;

            void InvokeTimerOver()
            {
                _loadingTimerPresenter.OnTimerOver -= InvokeTimerOver;
                
                _gameLoop.TimerOver.Invoke();
            }
        }

        private void StartInGameTimer()
        {
            _inGameTimerModel.StartTimer(_settings.InGameTimerLength);
            
            _inGameTimerPresenter.OnTimerOver += InvokeTimerOver;

            void InvokeTimerOver()
            {
                _inGameTimerPresenter.OnTimerOver -= InvokeTimerOver;
                
                _gameLoop.GameOver.Invoke();
            }
        }
    }
}