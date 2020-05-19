using Core.Interfaces;
using Galcon.Level.Planets;
using Galcon.Level.PlayerManagement.Ownership;

namespace Galcon.Level.PlayerManagement
{
    public interface  IPlayer : IGameObjectHost
    {
        IPlanetOwner owner { get; }

        void TrySelectPlanet(IPlanet planet);
        void TryDeselectPlanet(IPlanet planet);
        void OnPlanetClicked(IPlanet planet);
        void DeselectAll();
    }
}
