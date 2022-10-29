using System;
using System.Threading.Tasks;
using Game.Services.Sound.Enums;
using Game.Services.Sound.Interfaces;
using Game.UI.Timer.Interfaces;
using Input;
using Zenject;

namespace Game.UI.Timer
{
    public class TimerPresenter : IInitializable, ILateDisposable
    {
        public TimerPresenter(ITimerModel model, ITimerView view, ISoundService<ESoundType> soundService)
        {
            _model = model;
            _view = view;

            _soundService = soundService;
        }

        public event Action OnTimerOver = () => { };

        private const float MinTickSoundLength = 10f;
        private const float TickLength = 5f;
        private const float TickPeriod = 0.5f;
        
        private readonly ITimerModel _model;
        private readonly ITimerView _view;

        private readonly ISoundService<ESoundType> _soundService;

        private Controls _controls;
        
        public void Initialize()
        {
            _controls = new Controls();
            _controls.Enable();
            
            _model.OnTimerStarted += StartTimer;
        }

        public void LateDispose()
        {
            _model.OnTimerStarted -= StartTimer;
        }

        private async void StartTimer(float time)
        {
            _view.SetValue(time);
            
            _view.StartTimer(time);
            _view.OnTimerOver += Perform;

            if (time >= MinTickSoundLength)
            {
                await Task.Delay(TimeSpan.FromSeconds(time - TickLength));

                var count = TickLength / TickPeriod;
                for (var i = 0; i < count; i++)
                {
                    await Task.Delay(TimeSpan.FromSeconds(TickPeriod));
                    _soundService.PlayOneShot(ESoundType.Tick);
                }
            }

            void Perform()
            {
                _view.OnTimerOver -= Perform;
                OnTimerOver.Invoke();
            }
        }
    }
}