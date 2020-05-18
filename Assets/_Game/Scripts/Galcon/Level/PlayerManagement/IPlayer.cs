using Galcon.Level.Planets;

namespace Galcon.Level.PlayerManagement
{
    public interface  IPlayer
    {
        void TrySelectPlanet(IPlanet planet);
        void TryDeselectPlanet(IPlanet planet);
        void OnPlanetClicked(IPlanet planet);
        void DeselectAll();
    }
}
