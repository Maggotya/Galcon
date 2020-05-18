using UnityEngine;

namespace Galcon.Level.PlayerManagement.Ownership.Parameters
{
    [CreateAssetMenu(fileName = "PlanetPopulationVisibleParameters", menuName = "Parameters/PlanetPopulationVisible")]
    public class PlanetOwnersVisibleParameters : ScriptableObject, IPlanetOwnersVisibleParameters
    {
        [SerializeField] private string[] _PopulationVisibleFor;

        public string[] visibleFor => _PopulationVisibleFor;
    }
}
