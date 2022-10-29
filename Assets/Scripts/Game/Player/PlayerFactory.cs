using Game.Player.Interfaces;
using Game.Space.Interfaces;
using UnityEngine;
using Utils;
using Zenject;

namespace Game.Player
{
    public class PlayerFactory : IFactory<IPlayerView>
    {
        public PlayerFactory(PlayerView prefab, ISpaceProvider spaceProvider)
        {
            _prefab = prefab;
            _spaceProvider = spaceProvider;
        }

        private readonly PlayerView _prefab;
        private readonly ISpaceProvider _spaceProvider;
        
        public IPlayerView Create()
        {
            var position = _spaceProvider.GetRandomValidPosition();
            var realPosition = position.ProjectToPerspective();

            var result = Object.Instantiate(_prefab, realPosition, Quaternion.identity);
            return result;
        }
    }
}