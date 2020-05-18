using Galcon.Level.Planets;

namespace Galcon.Level.InitialConfiguration.Population
{
    public class PlanetsPopulator : IPlanetsPopulator
    {
        private readonly int _initialPopulation;

        //////////////////////////////////////////////////////

        public PlanetsPopulator(int initialPopulaton)
            => _initialPopulation = initialPopulaton;

        //////////////////////////////////////////////////////

        public void Populate(params IPlanet[] planets)
        {
            foreach (var planet in planets) {
                // это всего лишь трюк, чтобы вызвать события при обнулении населения
                planet.SetPopulation(_initialPopulation);
                if (planet.owner.isClear)
                    planet.SetPopulation(0);
            }
        }
    }
}
