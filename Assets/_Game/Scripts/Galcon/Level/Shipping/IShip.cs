using Core.Interfaces;
using Galcon.Level.Planets;
using UnityEngine;

namespace Galcon.Level.Shipping
{
    public interface  IShip : IGameObjectHost, IDestroyable
    {
        int population { get; }
        string owner { get; }

        int PopulateAndReturnRemainder(string ownerTag, Color color, int inputPopulation);
        void LaunchToPlanet(IPlanet planet);
    }
}
