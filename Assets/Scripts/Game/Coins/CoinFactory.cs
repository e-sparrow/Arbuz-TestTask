using Game.Coins.Interfaces;
using Game.Space.Interfaces;
using UnityEngine;
using Utils;
using Zenject;

namespace Game.Coins
{
    public class CoinFactory : IFactory<ICoinView>
    {
        public CoinFactory(CoinView prefab, ISpaceProvider spaceProvider)
        {
            _prefab = prefab;
            _spaceProvider = spaceProvider;
        }

        private readonly CoinView _prefab;
        private readonly ISpaceProvider _spaceProvider;
        
        public ICoinView Create()
        {
            var position = _spaceProvider.GetRandomValidPosition();
            var realPosition = position.ProjectToPerspective();
            
            var result = Object.Instantiate(_prefab, realPosition, Quaternion.identity);
            return result;
        }
    }
}