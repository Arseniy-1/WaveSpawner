using Code.Services;
using Code.Services.Random;
using Code.Services.StaticData;
using Code.Services.Time;
using Zenject;

namespace Code.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindServices();
        }

        private void BindServices()
        {
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<IRandomService>().To<UnityRandomService>().AsSingle();
            Container.Bind<ITimeService>().To<UnityTimeService>().AsSingle();
        }
    }
}