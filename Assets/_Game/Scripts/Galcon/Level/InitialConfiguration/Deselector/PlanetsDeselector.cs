using Galcon.Level.Planets;

namespace Galcon.Level.InitialConfiguration.Deselector
{
    class PlanetsDeselector : IPlanetsDeselector
    {
        public void DeselectPlanets(IPlanet[] planets)
        {
            foreach (var planet in planets)
                planet.SetSelected(false);
        }
    }
}
