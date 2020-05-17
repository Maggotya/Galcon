using Core.Modules.ConvexHull;
using Galcon.Level.InitialConfiguration;
using Galcon.Level.InitialConfiguration.Deselector;
using Galcon.Level.InitialConfiguration.Distribution;
using Galcon.Level.InitialConfiguration.Population;
using Galcon.Level.Parameters;
using Galcon.Level.Planets;
using Galcon.Level.Planets.Manager;
using Zenject;

namespace Galcon.Level.Installers
{
    class LevelInstaller : MonoInstaller<LevelInstaller>
    {
        [Inject] private ILevelParameters _parameters;

        ////////////////////////////////////////////////////////////////

        public override void InstallBindings()
        {
            Container.Bind<IPlanetsManager>().To<PlanetsManager>().FromComponentsInChildren().AsSingle();
            Container.Bind<IPlanetsConfigurator>().FromMethod(CreatePlanetsConfigurator).AsSingle();

            Container.Bind<ILevelManager>().To<LevelManager>().FromComponentOnRoot().AsSingle();
        }

        ////////////////////////////////////////////////////////////////

        private IPlanetsConfigurator CreatePlanetsConfigurator()
            => new PlanetsConfigurator(CreatePlanetsDistributor(), CreatePlanetsDeselector(), CreatePlanetsPopulator());

        private IPlanetsPopulator CreatePlanetsPopulator()
            => new PlanetsPopulator(_parameters.initialPopulationOnEveryPlanet);

        private IPlanetsDeselector CreatePlanetsDeselector()
            => new PlanetsDeselector();

        private IPlanetsForPlayersDistributor CreatePlanetsDistributor()
            => new PlanetsForPlayersDistributor(
                _parameters.players,
                _parameters.initialPlanetsEveryPlayerHas,
                CreateConvexHullBuilder());
        private IConvexHullBuilder<IPlanet> CreateConvexHullBuilder()
            => new ConvexHullBuilderByJarvisMethod<IPlanet>();
    }
}
