using Galcon.Level.Planets;

namespace Galcon.Level.InitialConfiguration.Distribution
{
    interface IPlanetsForPlayersDistributor
    {
        void Distribute(IPlanet[] planets);
    }
}
