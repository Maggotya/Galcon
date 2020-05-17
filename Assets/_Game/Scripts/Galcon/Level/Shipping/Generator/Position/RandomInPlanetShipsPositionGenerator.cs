using Galcon.Level.Planets;
using UnityEngine;

namespace Galcon.Level.Shipping.Generator.Position
{
    class RandomInPlanetShipsPositionGenerator : IShipsPositionGenerator
    {
        private readonly IPlanet _planet;

        private const float _MAX_DISTANCE_FROM_PLANET_BORDER = 0.05F;

        ////////////////////////////////////////////
        
        public RandomInPlanetShipsPositionGenerator(IPlanet planet)
        {
            _planet = planet;
        }

        ////////////////////////////////////////////

        public Vector2 GeneratePosition()
            => _planet.circle.GetRandomPointInDistance(_MAX_DISTANCE_FROM_PLANET_BORDER);
    }
}
