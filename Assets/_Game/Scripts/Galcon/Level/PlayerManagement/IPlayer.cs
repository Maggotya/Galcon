using Core.Interfaces;
using Galcon.Level.Planets;

namespace Galcon.Level.PlayerManagement
{
    public interface  IPlayer : IGameObjectHost
    {
        void TrySelectPlanet(IPlanet planet);
        void TryDeselectPlanet(IPlanet planet);
        void OnPlanetClicked(IPlanet planet);
        void DeselectAll();
    }
}
