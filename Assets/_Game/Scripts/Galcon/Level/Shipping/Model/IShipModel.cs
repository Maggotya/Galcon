using Galcon.Level.Planets;

namespace Galcon.Level.Shipping.Model
{
    public interface  IShipModel
    {
        int maxPopulation { get; }
        int population { get; set; }
        string ownerTag { get; set; }
        IPlanet targetPlanet { get; set; }
    }
}
