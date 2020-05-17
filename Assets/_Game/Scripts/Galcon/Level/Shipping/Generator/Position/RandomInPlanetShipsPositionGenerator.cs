using Galcon.Level.Planets;
using UnityEngine;

namespace Galcon.Level.Shipping.Generator.Position
{
    class RandomInPlanetShipsPositionGenerator : IShipsPositionGenerator
    {
        private readonly IPlanet _planet;

        ////////////////////////////////////////////
        
        public RandomInPlanetShipsPositionGenerator(IPlanet planet)
        {
            _planet = planet;
        }

        ////////////////////////////////////////////

        public Vector2 GeneratePosition()
            => _planet.circle.GetRandomPoint();
    }
}
