using Galcon.Level.Planets;

namespace Galcon.Level.InitialConfiguration
{
    public interface  IPlanetsConfigurator
    {
        void Configure(IPlanet[] planets);
    }
}
