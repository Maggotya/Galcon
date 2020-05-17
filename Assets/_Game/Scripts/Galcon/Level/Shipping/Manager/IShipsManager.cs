using Galcon.Level.Planets;
using UnityEngine;

namespace Galcon.Level.Shipping.Manager
{
    interface IShipsManager
    {
        IShip[] CreateShips(string ownerTag, Color color, int populationForShips);
        void LaunchShips(IShip[] ships, IPlanet targetPlanet);
        void LaunchShips(string ownerTag, Color color, int populationForShips, IPlanet targetPlanet);
    }
}
