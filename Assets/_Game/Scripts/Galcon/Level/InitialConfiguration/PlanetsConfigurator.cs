using Galcon.Level.InitialConfiguration.Deselector;
using Galcon.Level.InitialConfiguration.Distribution;
using Galcon.Level.InitialConfiguration.Population;
using Galcon.Level.Planets;

namespace Galcon.Level.InitialConfiguration
{
    class PlanetsConfigurator : IPlanetsConfigurator
    {
        private readonly IPlanetsPopulator _planetsPopulator;
        private readonly IPlanetsDeselector _planetsDeselector;
        private readonly IPlanetsForPlayersDistributor _planetsDistributor;

        /////////////////////////////////////////////////////////////////////////
        
        public PlanetsConfigurator(IPlanetsForPlayersDistributor planetsDistributor, IPlanetsDeselector planetsDeselector, 
            IPlanetsPopulator planetsPopulator)
        {
            _planetsPopulator = planetsPopulator;
            _planetsDeselector = planetsDeselector;
            _planetsDistributor = planetsDistributor;
        }

        /////////////////////////////////////////////////////////////////////////

        public void Configure(IPlanet[] planets)
        {
            _planetsDistributor.Distribute(planets);
            _planetsPopulator.Populate(planets);
            _planetsDeselector.DeselectPlanets(planets);
        }
    }
}
