using Core;
using Galcon.Level.Planets;

namespace Galcon.Level.InitialConfiguration.Deselector
{
    public class PlanetsDeselector : IPlanetsDeselector
    {
        public void DeselectPlanets(IPlanet[] planets)
        {
            foreach (var planet in planets)
                planet.SetSelected(false);
        }
    }
}
