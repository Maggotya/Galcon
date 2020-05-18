using System.Collections.Generic;
using Core;
using Core.Extensions;
using Galcon.Level.Planets;
using Galcon.Level.Shipping.Generator;
using UnityEngine;
using Zenject;

namespace Galcon.Level.Shipping.Manager
{
    public class ShipsManager : MyMonoBehaviour, IShipsManager
    {
        private IShipsGenerator _generator;

        ////////////////////////////////////////////////////////////
        
        [Inject]
        public void Construct(IShipsGenerator generator)
        {
            _generator = generator;
        }

        ////////////////////////////////////////////////////////////

        public IShip[] CreateShips(string ownerTag, Color color, int populationForShips)
        {
            var ships = new Queue<IShip>();

            while (populationForShips > 0 && _generator.TryGenerate(out var ship)) {
                populationForShips = ship.PopulateAndReturnRemainder(ownerTag, color, populationForShips);
                ships.Enqueue(ship);
            }

            Logging.Log(_source, "Created ships");
            return ships.ToArray();
        }
 
        public void LaunchShips(string ownerTag, Color color, int populationForShips, IPlanet targetPlanet)
            => LaunchShips(CreateShips(ownerTag, color, populationForShips), targetPlanet);

        public void LaunchShips(IShip[] ships, IPlanet targetPlanet)
        {
            foreach (var ship in ships)
                ship?.LaunchToPlanet(targetPlanet);

            Logging.Log(_source, "Launched ships");
        }
    }
}
