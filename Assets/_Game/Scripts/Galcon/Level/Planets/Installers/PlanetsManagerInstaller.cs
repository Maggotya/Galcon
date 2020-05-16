using Core.Modules.Dispenser;
using Galcon.Level.Planets.Creation;
using Galcon.Level.Planets.Creation.Generator;
using Galcon.Level.Planets.Creation.Parameters;
using Galcon.Level.ScreenView;
using UnityEngine;
using Zenject;

namespace Galcon.Level.Planets.Installers
{
    class PlanetsManagerInstaller : MonoInstaller<PlanetsManagerInstaller>
    {
        [SerializeField] private PlanetsGeneratorParameters _Parameters;

        [Inject] private IBorderedArea _borderedArea { get; set; }

        ////////////////////////////////////////////////////

        public override void InstallBindings()
        {
            Container.Bind<IPlanetsGeneratorParameters>().FromInstance(_Parameters).AsSingle();

            Container.BindFactory<IPlanet, PlanetsFactory>().FromComponentInNewPrefab(_Parameters.planetPrefab);
            Container.Bind<IPlanetsFactory>().To<PlanetsFactory>().FromResolve();

            Container.Bind<IPlanetsGenerator>().FromMethod(CreatePlanetsGenerator);
        }

        ////////////////////////////////////////////////////

        private IPlanetsGenerator CreatePlanetsGenerator(InjectContext context)
        {

            return new PlanetsGenerator(transform,
                _Parameters.minPlanetsCount, _Parameters.maxPlanetsCount,
                context.Container.Resolve<IPlanetsFactory>(),
                CreatePlanetsPositionsGenerator(),
                CreatePlanetsRadiusesGenerator(),
                new SpriteDispenser(_Parameters.possibleSprites));
        }

        private IPlanetsPositionsGenerator CreatePlanetsPositionsGenerator()
            => new PlanetsPositionsGenerator(_borderedArea, _Parameters.minDistanceBetweenPlanetsBorders);

        private IPlanetsRadiusesGenerator CreatePlanetsRadiusesGenerator()
            => new PlanetsRadiusesGenerator(_Parameters.minPlanetRadius, _Parameters.maxPlanetRadius, _Parameters.minDistanceBetweenPlanetsBorders);
    }
}
