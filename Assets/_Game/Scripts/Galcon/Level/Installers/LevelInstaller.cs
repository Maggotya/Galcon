using Core.Modules.ConvexHull;
using Galcon.Level.Finishing;
using Galcon.Level.InitialConfiguration;
using Galcon.Level.InitialConfiguration.Deselector;
using Galcon.Level.InitialConfiguration.Distribution;
using Galcon.Level.InitialConfiguration.Population;
using Galcon.Level.Parameters;
using Galcon.Level.Planets;
using Galcon.Level.Planets.Manager;
using Galcon.Level.PlayerManagement;
using UnityEngine;
using Zenject;

namespace Galcon.Level.Installers
{
    public class LevelInstaller : MonoInstaller<LevelInstaller>
    {
        [SerializeField] private NavMeshRebaker _NavMeshRebaker;
        [SerializeField] private Player _Player;

        [Inject] private ILevelParameters _parameters;

        ////////////////////////////////////////////////////////////////

        public override void InstallBindings()
        {
            Container.Bind<IPlayer>().To<Player>().FromInstance(_Player).AsSingle();
            Container.Bind<NavMeshRebaker>().FromInstance(_NavMeshRebaker).AsSingle();
            Container.Bind<ILevelFinishedManager>().To<LevelFinishedManager>().AsSingle();

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
