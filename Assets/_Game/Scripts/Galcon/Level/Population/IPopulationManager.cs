using Core.Extensions;
using UnityEngine.Events;

namespace Galcon.Level.Population
{
    interface IPopulationManager
    {
        UnityEvent onPopulationExterminated { get; set; }
        UnityEvent onEmergenceOfPopulation { get; set; }
        IntUnityEvent onPopulationIncreased { get; set; }

        void AcceptOpponents(int population);
        void AcceptAllies(int population);
        int EvictPopulationForShips();
        void Clear();
    }
}
