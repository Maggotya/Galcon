using System.Collections.Generic;
using System.Linq;
using Galcon.Level.ScreenView;
using UnityEngine;

namespace Galcon.Level.Planets.Creation.Generator
{
    class PlanetsPositionsGenerator : IPlanetsPositionsGenerator
    {
        private readonly IBorderedArea _borderedArea;
        private readonly float _minDistanceBetweenPlanetsBorders;

        public float lastMinDistanceToScreenBorders { get; private set; }
        public float lastMinDistanceToPlanetsBorders { get; private set; }
        public float lastMinDistanceToObjectsBorders { get; private set; }

        ////////////////////////////////////////////

        public PlanetsPositionsGenerator(IBorderedArea borderedArea, float minDistanceBetweenPlanetsBorders)
        {
            _borderedArea = borderedArea;
            _minDistanceBetweenPlanetsBorders = minDistanceBetweenPlanetsBorders;
        }

        ////////////////////////////////////////////

        public bool TryGeneratePosition(IEnumerable<IPlanet> planets, out Vector2 position)
        {
            ResetCounter();

            while (!TryIterate(planets, out position) && _isCounterInRange)
                IncreaseCounter();

            return _isCounterInRange;
        }

        #region ITERATING
        private bool TryIterate(IEnumerable<IPlanet> planets, out Vector2 position)
        {
            position = _borderedArea.area.random;
            var minDistance = GetMinDistance(planets, position);

            return minDistance >= _minDistanceBetweenPlanetsBorders;
        }

        private float GetMinDistance(IEnumerable<IPlanet> planets, Vector2 point)
        {
            lastMinDistanceToScreenBorders = GetMinDistanceToBorders(point);
            lastMinDistanceToPlanetsBorders = GetMinDistanceToPlanets(planets, point);
            lastMinDistanceToObjectsBorders = Mathf.Min(lastMinDistanceToScreenBorders, lastMinDistanceToPlanetsBorders);

            return lastMinDistanceToObjectsBorders;
        }

        private float GetMinDistanceToBorders(Vector2 point)
            => _borderedArea.GetMinDistanceToBorder(point);

        private float GetMinDistanceToPlanets(IEnumerable<IPlanet> planets, Vector2 point)
            => planets.Any() ? 
            planets.Select(p => p.DistanceFromBorder(point)).Min() : 
            Mathf.Infinity;
        #endregion // ITERATING

        #region COUNTING
        private const int _MAX_ITERATIONS = 100;
        private int _counter = 0;

        private bool _isCounterInRange => _counter <= _MAX_ITERATIONS;

        private void ResetCounter()
            => _counter = 0;

        private void IncreaseCounter()
            => ++_counter;
        #endregion // COUNTING
    }
}
