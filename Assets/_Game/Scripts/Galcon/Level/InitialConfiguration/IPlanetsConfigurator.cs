using Galcon.Level.Planets;

namespace Galcon.Level.InitialConfiguration
{
    interface IPlanetsConfigurator
    {
        void Configure(IPlanet[] planets);
    }
}
