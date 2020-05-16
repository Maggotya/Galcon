using UnityEngine;

namespace Galcon.Level.Planets.Model
{
    interface IPlanetModel
    {
        float radius { get; set; }
        Color color { get; set; }
    }
}
