using System.Collections.Generic;
using UnityEngine;

namespace Galcon.Level.Planets.Creation.Generator
{
    public interface  IPlanetsPositionsGenerator
    {
        float lastMinDistanceToScreenBorders { get; }
        float lastMinDistanceToPlanetsBorders { get; }
        float lastMinDistanceToObjectsBorders { get; }

        bool TryGeneratePosition(IEnumerable<IPlanet> planets, out Vector2 position);
    }
}
