using Game.Coins;
using Game.Constants;
using Game.Level;
using Game.Player;
using Game.Space;
using Game.UI.Counter;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        [Header("General")]
        [SerializeField] private MonoGameLoop gameLoop;

        [Header("Views")]
        [SerializeField] private LevelView levelView;
        [SerializeField] private CounterView counterView;
        
        [Header("Prefabs")]
        [SerializeField] private PlayerView playerPrefab;
        [SerializeField] private CoinView coinPrefab;
        [SerializeField] private AudioSource audioSourcePrefab;
        
        public override void InstallBindings()
        {
            PreInstall();

            InstallLevel();
            InstallPlayer();
            InstallCoins();
            InstallCounter();

            Container
                .BindMemoryPool<AudioSource, MemoryPool<AudioSource>>()
                .WithInitialSize(8)
                .WithMaxSize(16)
                .FromComponentInNewPrefab(audioSourcePrefab)
                .UnderTransformGroup(ProjectConstants.AudioPoolTransformGroup);
        }

        private void PreInstall()
        {
            Container
                .BindInterfacesTo<MonoGameLoop>()
                .FromInstance(gameLoop)
                .AsSingle();

            Container
                .BindInterfacesTo<SpaceProvider>()
                .AsSingle();
        }

        private void InstallLevel()
        {
            Container
                .BindInterfacesTo<LevelView>()
                .FromInstance(levelView)
                .AsSingle();

            Container
                .BindInterfacesTo<LevelPresenter>()
                .AsSingle()
                .NonLazy();
        }
        
        private void InstallPlayer()
        {
            Container
                .BindInterfacesTo<PlayerFactory>()
                .AsSingle()
                .WithArguments(playerPrefab);

            Container
                .BindInterfacesTo<PlayerPresenter>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallCoins()
        {
            Container
                .BindInterfacesTo<CoinFactory>()
                .AsSingle()
                .WithArguments(coinPrefab);

            Container
                .BindInterfacesTo<CoinsSystem>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallCounter()
        {
            Container
                .BindInterfacesTo<CounterView>()
                .FromInstance(counterView)
                .AsSingle();

            Container
                .BindInterfacesTo<CounterModel>()
                .AsSingle();

            Container
                .BindInterfacesTo<CounterPresenter>()
                .AsSingle()
                .NonLazy();
        }
    }
}
