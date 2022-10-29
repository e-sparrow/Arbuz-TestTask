using Game.Interfaces;
using Game.Player.Interfaces;
using Game.Settings.Interfaces;
using Game.Space.Interfaces;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Player
{
    public class PlayerPresenter : IInitializable, IFixedTickable, ILateDisposable
    {
        public PlayerPresenter(IFactory<IPlayerView> playerFactory, IPlayerSettings settings, ISpaceProvider spaceProvider, IGameLoop gameLoop)
        {
            _view = playerFactory.Create();
            _settings = settings;

            _spaceProvider = spaceProvider;
            _gameLoop = gameLoop;
        }

        private readonly IPlayerView _view;
        private readonly IPlayerSettings _settings;

        private readonly ISpaceProvider _spaceProvider;
        private readonly IGameLoop _gameLoop;

        private Controls _controls;

        private bool _isMoving = false;
        
        public void Initialize()
        {
            _gameLoop.TimerOver.OnInvoke += Enable;
        }

        public void LateDispose()
        {
            _controls?.Disable();
            
            _controls.Main.Movement.started -= StartMove;
            _controls.Main.Movement.canceled -= CancelMove;
        }

        private void Enable()
        {
            _gameLoop.TimerOver.OnInvoke -= Enable;
            
            _controls = new Controls();
            _controls.Enable();

            _controls.Main.Movement.started += StartMove;
            _controls.Main.Movement.canceled += CancelMove;
        }

        private void StartMove(InputAction.CallbackContext context)
        {
            _isMoving = true;
        }

        private void CancelMove(InputAction.CallbackContext context)
        {
            _isMoving = false;
        }

        public void FixedTick()
        {
            if (!_isMoving) return;
            
            var value = _controls.Main.Movement.ReadValue<Vector2>() * Time.fixedDeltaTime * _settings.Speed;

            var position = _view.Position + value;
            if (_spaceProvider.IsPositionValid(position))
            {
                _view.Move(value);
            }
        }
    }
}