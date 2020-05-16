
using UnityEngine;

namespace Galcon.Level.Planets.Manager
{
    interface IPlanetsManager
    {
        IPlanet[] planets { get; }

        void GeneratePlanets();
        IPlanet GetPlanetByPosition(Vector2 point);
        void Clear();
    }
}
