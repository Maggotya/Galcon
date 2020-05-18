using Core.Handlers;
using Galcon.Level.Population.Model;
using Galcon.Level.Population.Parameters;
using Galcon.Level.Population.View;
using UnityEngine;
using Zenject;

namespace Galcon.Level.Population.Installers
{
    public class PopulationInstaller : MonoInstaller<PopulationInstaller>
    {
        [SerializeField] private PopulationParameters _Parameters;
        [SerializeField] private GameObject _ViewModel;

        public override void InstallBindings()
        {
            Container.Bind<IPopulationParameters>().To<PopulationParameters>().FromInstance(_Parameters).AsSingle();

            Container.Bind<IPopulationModel>().To<PopulationModel>().AsSingle();
            Container.Bind<IPopulationView>().To<PopulationView>().AsSingle().WithArguments(_ViewModel);

            Container.Bind<ITimerHandler>().FromMethod(CreateTimerHandler).AsSingle();
            Container.Bind<float>().FromInstance(_Parameters.proportionToEvictOnShips).WhenInjectedInto<PopulationManager>();
            Container.Bind<int>().FromInstance(_Parameters.populationPerInterval).WhenInjectedInto<PopulationManager>();
        }

        private ITimerHandler CreateTimerHandler()
            => new TimerHandler(timerCapacity: _Parameters.updateIntervalInSeconds, repeatable: true);
    }
}
