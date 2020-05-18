using UnityEngine;

namespace Galcon.Level.Planets.Model
{
    public interface  IPlanetModel
    {
        float radius { get; set; }
        Color color { get; set; }
    }
}
