using System.Linq;
using Core;
using Core.Extensions;
using Galcon.Level.Planets.Creation.Generator;
using UnityEngine;
using Zenject;

namespace Galcon.Level.Planets.Manager
{
    class PlanetsManager : MyMonoBehaviour, IPlanetsManager
    {
        private IPlanetsGenerator _generator { get; set; }
        public IPlanet[] planets { get; private set; }

        ///////////////////////////////////////

        [Inject]
        public void Construct(IPlanetsGenerator generator)
        {
            _generator = generator;
        }

        ///////////////////////////////////////

        public void GeneratePlanets()
        {
            Clear();
            planets = _generator.Generate();
            Logging.Log(_source, "Generated planets");
        }

        public void Clear()
        {
            if (planets == null)
                return;

            foreach (var planet in planets)
                Destroy(planet.gameObject);

            planets = new IPlanet[0];
            Logging.Log(_source, "Cleared planets");
        }

        public IPlanet GetPlanetByPosition(Vector2 position)
            => planets.FirstOrDefault(p => p.Contains(position));
    }
}
