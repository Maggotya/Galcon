using Galcon.Level.Planets;

namespace Galcon.Level.InitialConfiguration.Distribution
{
    public interface  IPlanetsForPlayersDistributor
    {
        void Distribute(IPlanet[] planets);
    }
}
