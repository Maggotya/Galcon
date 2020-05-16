using Galcon.Level.Planets;

namespace Galcon.Level.InitialConfiguration.Deselector
{
    interface IPlanetsDeselector
    {
        void DeselectPlanets(IPlanet[] planets);
    }
}
