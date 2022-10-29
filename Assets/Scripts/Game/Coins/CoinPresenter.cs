using Game.Coins.Interfaces;
using Zenject;

namespace Game.Coins
{
    public class CoinPresenter : IInitializable, ILateDisposable
    {
        public CoinPresenter(ICoinModel model, ICoinView view)
        {
            _model = model;
            _view = view;
        }

        private readonly ICoinModel _model;
        private readonly ICoinView _view;
        
        public void Initialize()
        {
            _view.OnCoinTaken += _model.Take;
        }

        public void LateDispose()
        {
            _view.OnCoinTaken -= _model.Take;
        }
    }
}