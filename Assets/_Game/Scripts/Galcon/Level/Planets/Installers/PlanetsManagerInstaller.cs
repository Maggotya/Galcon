using Core.Modules.Dispenser;
using Galcon.Level.Installers;
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
        [Inject(Id = PrefabType.Planet)] private GameObject _prefab { get; set; }
        [Inject] private IPlanetsGeneratorParameters _parameters { get; set; }
        [Inject] private IBorderedArea _borderedArea { get; set; }

        ////////////////////////////////////////////////////

        public override void InstallBindings()
        {
            Container.BindFactory<IPlanet, PlanetsFactory>().FromComponentInNewPrefab(_prefab);
            Container.Bind<IPlanetsFactory>().To<PlanetsFactory>().FromResolve();

            Container.Bind<IPlanetsGenerator>().FromMethod(CreatePlanetsGenerator);
        }

        ////////////////////////////////////////////////////

        private IPlanetsGenerator CreatePlanetsGenerator(InjectContext context)
        {

            return new PlanetsGenerator(transform,
                _parameters.minPlanetsCount, _parameters.maxPlanetsCount,
                context.Container.Resolve<IPlanetsFactory>(),
                CreatePlanetsPositionsGenerator(),
                CreatePlanetsRadiusesGenerator(),
                new SpriteDispenser(_parameters.possibleSprites));
        }

        private IPlanetsPositionsGenerator CreatePlanetsPositionsGenerator()
            => new PlanetsPositionsGenerator(_borderedArea, _parameters.minDistanceBetweenPlanetsBorders);

        private IPlanetsRadiusesGenerator CreatePlanetsRadiusesGenerator()
            => new PlanetsRadiusesGenerator(_parameters.minPlanetRadius, _parameters.maxPlanetRadius, _parameters.minDistanceBetweenPlanetsBorders);
    }
}
