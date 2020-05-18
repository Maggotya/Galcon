using UnityEngine;

namespace Galcon.Level.Parameters
{
    [CreateAssetMenu(fileName = "LevelParameters", menuName = "Parameters/Level")]
    public class LevelParameters : ScriptableObject, ILevelParameters
    {
        [SerializeField] private string[] _Players;
        [SerializeField] [Min(1)] private int _InitialPlanetsEveryPlayerHas;
        [SerializeField] [Min(1)] private int _InitialPopulationOnEveryPlanet;

        public string[] players => _Players;
        public int initialPlanetsEveryPlayerHas => _InitialPlanetsEveryPlayerHas;
        public int initialPopulationOnEveryPlanet => _InitialPopulationOnEveryPlanet;
    }
}
