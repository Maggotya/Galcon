using UnityEngine;

namespace Galcon.Level.Planets.Creation.Generator
{
    class PlanetsRadiusesGenerator : IPlanetsRadiusesGenerator
    {
        private readonly float _minRadius;
        private readonly float _maxRadius;
        private readonly float _minDistanceBetweenPlanetsBorders;

        /////////////////////////////////////////

        public PlanetsRadiusesGenerator(float minRadius, float maxRadius, float minDistanceBetweenPlanetsBorders)
        {
            _minRadius = minRadius;
            _maxRadius = maxRadius;
            _minDistanceBetweenPlanetsBorders = minDistanceBetweenPlanetsBorders;
        }

        /////////////////////////////////////////

        public bool TryGenerateRadius(float distanceFromCenterToNearestPlanetBorder, out float size)
        {
            size = 0;
            var maxValueOfRadius = distanceFromCenterToNearestPlanetBorder - _minDistanceBetweenPlanetsBorders;
            var maxRadius = Mathf.Min(_maxRadius, maxValueOfRadius);

            if (maxRadius < _minRadius)
                return false;

            size = Random.Range(_minRadius, maxRadius);
            return true;
        }
    }
}
