using System.Collections.Generic;
using Game.Coins.Interfaces;
using Game.Interfaces;
using Game.Services.Sound.Enums;
using Game.Services.Sound.Interfaces;
using Game.Settings.Interfaces;
using Game.UI.Counter.Interfaces;
using Zenject;

namespace Game.Coins
{
    public class CoinsSystem : IInitializable, ILateDisposable
    {
        public CoinsSystem(IFactory<ICoinView> coinFactory, ILevelSettings levelSettings, ICounterModel counterModel, IGameLoop gameLoop, ISoundService<ESoundType> soundService)
        {
            _coinFactory = coinFactory;
            _levelSettings = levelSettings;
            _counterModel = counterModel;
            _gameLoop = gameLoop;
            _soundService = soundService;
        }

        private readonly IFactory<ICoinView> _coinFactory;
        private readonly ILevelSettings _levelSettings;
        private readonly ICounterModel _counterModel;
        private readonly IGameLoop _gameLoop;
        private readonly ISoundService<ESoundType> _soundService;

        private readonly IList<CoinPresenter> _presenters = new List<CoinPresenter>();

        public void Initialize()
        {
            for (var i = 0; i < _levelSettings.CoinsCount; i++)
            {
                var view = _coinFactory.Create();
                
                var model = new CoinModel();
                model.OnCoinTaken += TakeCoin;
                
                var presenter = new CoinPresenter(model, view);
                presenter.Initialize();
                
                _presenters.Add(presenter);
            }

            _counterModel.OnCountChanged += CheckCount;
        }

        public void LateDispose()
        {
            foreach (var presenter in _presenters)
            {
                presenter.LateDispose();
            }
            
            _presenters.Clear();
            
            _counterModel.OnCountChanged -= CheckCount;
        }

        private void TakeCoin()
        {
            _counterModel.Decrement();
            _soundService.PlayOneShot(ESoundType.Collect);
        }

        private void CheckCount(int count)
        {
            if (count <= 0)
            {
                _gameLoop.GameOver.Invoke();
            }
        }
    }
}