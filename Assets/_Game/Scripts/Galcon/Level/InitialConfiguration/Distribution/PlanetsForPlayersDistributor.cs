using System.Collections.Generic;
using System.Linq;
using Core.Modules.ConvexHull;
using Galcon.Level.Planets;

using Random = UnityEngine.Random;

namespace Galcon.Level.InitialConfiguration.Distribution
{
    class PlanetsForPlayersDistributor : IPlanetsForPlayersDistributor
    {
        private readonly string[] _players;
        private readonly int _planetsForPerPlayer;
        private readonly IConvexHullBuilder<IPlanet> _convexHullBuilder;

        //////////////////////////////////////////////////////////////
        
        public PlanetsForPlayersDistributor(string[] players, int planetsForPerPlayer, IConvexHullBuilder<IPlanet> convexHullBuilder)
        {
            _players = players;
            _planetsForPerPlayer = planetsForPerPlayer;
            _convexHullBuilder = convexHullBuilder;
        }

        //////////////////////////////////////////////////////////////

        public void Distribute(IPlanet[] planets)
        {
            var startPlanets = new Queue<IPlanet>();
            var availablePlanetsToStart = GetSetOfPlanetToStartForPlayers(planets);

            foreach (var player in _players) {
                var startPlanet = DistributeStartPlanetForPlayer(player, availablePlanetsToStart);
                startPlanets.Enqueue(startPlanet);
            }

            foreach (var planet in startPlanets)
                DistributeLeftPlanetsForPlayer(planet, planets);
        }

        //////////////////////////////////////////////////////////////

        private List<IPlanet> GetSetOfPlanetToStartForPlayers(IPlanet[] planets)
        {
            var planetsOfHull = new HashSet<IPlanet>(_convexHullBuilder.Build(planets));

            while (planetsOfHull.Count() < _players.Length) {
                var innerConvexHull = _convexHullBuilder.Build(planets.Except(planetsOfHull));
                planetsOfHull.UnionWith(innerConvexHull);
            }

            return planetsOfHull.ToList();
        }

        private IPlanet DistributeStartPlanetForPlayer(string player, List<IPlanet> availablePlanetsToStart)
        {
            var index = Random.Range(0, availablePlanetsToStart.Count());
            var startPlanet = availablePlanetsToStart[index];
            AssignPlanetToPlayer(startPlanet, player);

            availablePlanetsToStart.RemoveAt(index);
            return startPlanet;
        }

        private void DistributeLeftPlanetsForPlayer(IPlanet startPlanet, IPlanet[] allPlanets)
        {
            var freePlanets = allPlanets.Where(p => p.owner.isClear).ToList();

            var comparer = new PlanetsComparerByDistanceToPoint(startPlanet.gameObject.transform.position);
            freePlanets.Sort(comparer);

            var neededPlanets = freePlanets.Take(_planetsForPerPlayer - 1);

            foreach(var planet in neededPlanets)
                AssignPlanetToPlayer(planet, startPlanet.owner.ownerTag);
        }

        private void AssignPlanetToPlayer(IPlanet planet, string player)
            => planet.owner.SetTag(player);
    }
}
