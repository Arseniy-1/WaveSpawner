using Code.Services.Random;
using Code.Services.SceneLoader;
using Code.Services.StaticData;
using Code.Services.Time;
using Code.Spawners.Enemy;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner
    {
        [SerializeField] private Player.Player _player;
        
        public override void InstallBindings()
        {
            BindServices();
            BindFactories();
            BindGameplay();
        }

        private void BindServices()
        {
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<IRandomService>().To<UnityRandomService>().AsSingle();
            Container.Bind<ITimeService>().To<UnityTimeService>().AsSingle();
            Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        }

        private void BindFactories()
        {
            Container.Bind<IEnemyFabric>().To<EnemyFabric>().AsSingle();
        }

        private void BindGameplay()
        {
            Container.Bind<Player.Player>().FromInstance(_player).AsSingle();
        }
    }
}