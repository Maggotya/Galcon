using Core.ScriptableObjects;
using UnityEngine;

namespace Galcon.Level.Shipping.Parameters
{
    [CreateAssetMenu(fileName = "ShipParameters", menuName = "Parameters/Ship")]
    class ShipParameters : ScriptableObject, IShipParameters
    {
        [SerializeField][Min(0)] private int _PopulationCapacity;
        [SerializeField] private SpeedConfigs _SpeedConfigs;

        public int populationCapaciy => _PopulationCapacity;
        public ISpeedConfigs speedConfigs => _SpeedConfigs;
    }
}
