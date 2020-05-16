using UnityEngine;

namespace Galcon.Level.Population.Parameters
{
    [CreateAssetMenu(fileName = "PopulationParameters", menuName = "Parameters/Population")]
    class PopulationParameters : ScriptableObject, IPopulationParameters
    {
        [SerializeField] [Range(0f, 1f)] private float _ProportionToEvictOnShips;
        [SerializeField][Min(0)] private int _PopulationPerInterval;
        [SerializeField][Min(0)] private float _UpdateIntervalInSeconds;


        public float proportionToEvictOnShips => _ProportionToEvictOnShips;
        public int populationPerInterval => _PopulationPerInterval;
        public float updateIntervalInSeconds => _UpdateIntervalInSeconds;
    }
}
