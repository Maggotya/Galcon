using Core.Components;
using Galcon.Level.Planets.Model;
using Galcon.Level.Planets.View;
using Galcon.Level.PlayerManagement.Ownership;
using Galcon.Level.Population;
using Galcon.Level.Shipping.Manager;
using UnityEngine;
using Zenject;

namespace Galcon.Level.Planets.Installers
{
    public class PlanetInstaller : MonoInstaller<PlanetInstaller>
    {
        [SerializeField] private GameObject _ViewModel;

        public override void InstallBindings()
        {
            Container.Bind<IPlanetModel>().To<PlanetModel>().AsSingle();
            Container.Bind<IPlanetView>().To<PlanetView>().AsSingle().WithArguments(_ViewModel);

            Container.Bind<SelectableObject>().FromComponentsInChildren().AsSingle();
            Container.Bind<IShipsManager>().To<ShipsManager>().FromComponentsInChildren().AsSingle();

            Container.Bind<IPlanetOwner>().To<PlanetOwner>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<IPopulationManager>().To<PopulationManager>().FromComponentInChildren().AsSingle();

            Container.Bind<IPlanet>().To<Planet>().FromComponentOn(gameObject).AsSingle();

            // FromComponentOnRoot при инъекции в субкотейнеры пытается получить компонент на Root субконтейнера.
            // не знаю, баг это или фича, но мне нужен компонент с конкретно этого объекта
        }
    }
}
