using Galcon.Level.Planets;

namespace Galcon.Level.InitialConfiguration.Population
{
    public interface  IPlanetsPopulator
    {
        void Populate(params IPlanet[] planets);
    }
}
