using Game.Settings;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    [CreateAssetMenu(menuName = "Installers/Settings", fileName = "SettingsInstaller")]
    public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
    {
        [SerializeField] private SerializableLevelSettings levelSettings;
        [SerializeField] private SerializablePlayerSettings playerSettings;
        [SerializeField] private SerializableSpaceSettings spaceSettings;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<SerializableLevelSettings>()
                .FromInstance(levelSettings)
                .AsSingle();
            
            Container
                .BindInterfacesTo<SerializablePlayerSettings>()
                .FromInstance(playerSettings)
                .AsSingle();

            Container
                .BindInterfacesTo<SerializableSpaceSettings>()
                .FromInstance(spaceSettings)
                .AsSingle();
        }
    }
}