using System.Collections.Generic;
using Galcon.Level.Planets;
using UnityEngine;

namespace Galcon.Level.InitialConfiguration.Distribution
{
    class PlanetsComparerByDistanceToPoint : IComparer<IPlanet>
    {
        private readonly Vector2 _point;

        ////////////////////////////////////////////
        
        public PlanetsComparerByDistanceToPoint(Vector2 poins)
        {
            _point = poins;
        }

        ////////////////////////////////////////////

        public int Compare(IPlanet x, IPlanet y)
        {
            if (x == null && y == null)
                return 0;
            if (x != null && y == null)
                return 1;
            if (x == null && y != null)
                return -1;

            var xDistance = Vector2.Distance(_point, x.gameObject.transform.position);
            var yDistance = Vector2.Distance(_point, y.gameObject.transform.position);

            if (xDistance < yDistance)
                return -1;
            if (xDistance > yDistance)
                return 1;

            return 0;
        }
    }
}
