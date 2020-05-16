
using UnityEngine;

namespace Galcon.Level.Planets.Creation.Parameters
{
    interface IPlanetsGeneratorParameters
    {
        GameObject planetPrefab { get; }
        float minPlanetRadius { get; }
        float maxPlanetRadius { get; }
        float minDistanceBetweenPlanetsBorders { get; }
        int minPlanetsCount { get; }
        int maxPlanetsCount { get; }
        Sprite[] possibleSprites { get; }
    }
}
