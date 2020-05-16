using Core.Modules.ConvexHull;
using Galcon.Level.InitialConfiguration;
using Galcon.Level.InitialConfiguration.Deselector;
using Galcon.Level.InitialConfiguration.Distribution;
using Galcon.Level.InitialConfiguration.Population;
using Galcon.Level.Parameters;
using Galcon.Level.Planets;
using Galcon.Level.Planets.Manager;
using Galcon.Level.PlayerManagement.Ownership;
using Galcon.Level.ScreenView;
using UnityEngine;
using Zenject;

namespace Galcon.Level.Installers
{
    class LevelInstaller : MonoInstaller<LevelInstaller>
    {
        [SerializeField] private PlanetOwnersParameters _PlanetOwners;
        [SerializeField] private LevelParameters _Parameters;
        [SerializeField] private BorderedArea _BorderedArea;
        [SerializeField] private Camera _Camera;

        ////////////////////////////////////////////////////////////////

        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(_Camera).AsSingle();
            Container.Bind<IBorderedArea>().FromInstance(_BorderedArea).AsSingle();
            Container.Bind<ILevelParameters>().FromInstance(_Parameters).AsSingle();
            Container.Bind<IPlanetOwnersParameters>().FromInstance(_PlanetOwners).AsSingle();

            Container.Bind<IPlanetsManager>().To<PlanetsManager>().FromComponentsInChildren().AsSingle();
            Container.Bind<IPlanetsConfigurator>().FromMethod(CreatePlanetsConfigurator).AsSingle();

            Container.Bind<ILevelManager>().To<LevelManager>().FromComponentOnRoot().AsSingle();
        }

        ////////////////////////////////////////////////////////////////

        private IPlanetsConfigurator CreatePlanetsConfigurator()
            => new PlanetsConfigurator(CreatePlanetsDistributor(), CreatePlanetsDeselector(), CreatePlanetsPopulator());

        private IPlanetsPopulator CreatePlanetsPopulator()
            => new PlanetsPopulator(_Parameters.initialPopulationOnEveryPlanet);

        private IPlanetsDeselector CreatePlanetsDeselector()
            => new PlanetsDeselector();

        private IPlanetsForPlayersDistributor CreatePlanetsDistributor()
            => new PlanetsForPlayersDistributor(
                _Parameters.players, 
                _Parameters.initialPlanetsEveryPlayerHas,
                CreateConvexHullBuilder());
        private IConvexHullBuilder<IPlanet> CreateConvexHullBuilder()
            => new ConvexHullBuilderByJarvisMethod<IPlanet>();
    }
}
