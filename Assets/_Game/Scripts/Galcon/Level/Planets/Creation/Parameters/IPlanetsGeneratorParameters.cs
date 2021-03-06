﻿
using UnityEngine;

namespace Galcon.Level.Planets.Creation.Parameters
{
    public interface  IPlanetsGeneratorParameters
    {
        float minPlanetRadius { get; }
        float maxPlanetRadius { get; }
        float minDistanceBetweenPlanetsBorders { get; }
        int minPlanetsCount { get; }
        int maxPlanetsCount { get; }
        Sprite[] possibleSprites { get; }
    }
}
