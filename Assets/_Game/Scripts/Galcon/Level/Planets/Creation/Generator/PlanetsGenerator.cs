using System.Collections.Generic;
using Core.Modules.Dispenser;
using UnityEngine;

namespace Galcon.Level.Planets.Creation.Generator
{
    class PlanetsGenerator : IPlanetsGenerator
    {
        private readonly int _minCount;
        private readonly int _maxCount;
        private readonly Transform _container;
        private readonly IPlanetsFactory _factory;
        private readonly IDispenser<Sprite> _spriteDispenser;
        private readonly IPlanetsPositionsGenerator _positionsGenerator;
        private readonly IPlanetsRadiusesGenerator _sizesGenerator;

        /////////////////////////////////////////////////

        public PlanetsGenerator(Transform container, int minCount, int maxCount, IPlanetsFactory factory, 
            IPlanetsPositionsGenerator positionsGenerator, IPlanetsRadiusesGenerator sizesGenerator, IDispenser<Sprite> spriteDispenser)
        {
            _minCount = minCount;
            _maxCount = maxCount;
            _container = container;
            _factory = factory;
            _spriteDispenser = spriteDispenser;
            _positionsGenerator = positionsGenerator;
            _sizesGenerator = sizesGenerator;
        }

        /////////////////////////////////////////////////

        public IPlanet[] Generate()
        {
            var planets = new Queue<IPlanet>();
            var count = GenerateCountOfPlanets();

            for (var i = 0; i < count; i++)
                if (TryGeneratePlanet(planets, out var planet))
                    planets.Enqueue(planet);

            return planets.ToArray();
        }

        /////////////////////////////////////////////////

        private int GenerateCountOfPlanets()
            => Random.Range(_minCount, _maxCount + 1);

        private bool TryGeneratePlanet(IEnumerable<IPlanet> planets, out IPlanet planet)
        {
            planet = GeneratePlanet(planets);
            return planet != default;
        }

        private IPlanet GeneratePlanet(IEnumerable<IPlanet> planets)
        {
            var maxIterations = 100;
            var counter = 0;

            while (counter++ < maxIterations) {
                if (_positionsGenerator.TryGeneratePosition(planets, out var position) == false)
                    continue;

                if (_sizesGenerator.TryGenerateRadius(_positionsGenerator.lastMinDistanceToObjectsBorders, out var radius) == false)
                    continue;

                return GeneratePlanet(position, radius, _spriteDispenser.Dispense());
            }

            return default;
        }

        private IPlanet GeneratePlanet(Vector2 position, float radius, Sprite sprite)
        {
            var planet = _factory.Create();
            planet.gameObject.transform.parent = _container;
            planet.gameObject.transform.position = position;
            planet.SetSprite(sprite);
            planet.SetRadius(radius);

            return planet;
        }
    }
}