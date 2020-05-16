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
    class PlanetInstaller : MonoInstaller<PlanetInstaller>
    {
        [SerializeField] private GameObject _Model;

        public override void InstallBindings()
        {
            Container.Bind<IPlanetModel>().To<PlanetModel>().AsSingle();
            Container.Bind<IPlanetView>().To<PlanetView>().AsSingle().WithArguments(_Model);

            Container.Bind<SelectableObject>().FromComponentsInChildren().AsSingle();
            Container.Bind<IShipsManager>().To<ShipsManager>().FromComponentInChildren().AsSingle();
            Container.Bind<IPopulationManager>().To<PopulationManager>().FromComponentInChildren().AsSingle();
            Container.Bind<IPlanetOwner>().To<PlanetOwner>().FromComponentOnRoot().AsSingle();
            Container.Bind<IPlanet>().To<Planet>().FromComponentOnRoot().AsSingle();
        }
    }
}
