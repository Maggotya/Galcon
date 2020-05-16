using Galcon.Level.Planets;

namespace Galcon.Level.InitialConfiguration.Population
{
    interface IPlanetsPopulator
    {
        void Populate(params IPlanet[] planets);
    }
}
