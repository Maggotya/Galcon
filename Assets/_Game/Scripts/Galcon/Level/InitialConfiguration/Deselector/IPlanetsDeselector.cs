using Galcon.Level.Planets;

namespace Galcon.Level.InitialConfiguration.Deselector
{
    public interface  IPlanetsDeselector
    {
        void DeselectPlanets(IPlanet[] planets);
    }
}
