using Core.Extensions;
using UnityEngine.Events;

namespace Galcon.Level.Population
{
    interface IPopulationManager
    {
        int population { get; }
        UnityEvent onPopulationExterminated { get; set; }
        UnityEvent onEmergenceOfPopulation { get; set; }
        IntUnityEvent onPopulationIncreased { get; set; }

        int AcceptOpponentsAndReturnRemainder(int population);
        int AcceptAlliesAndReturnRemainder(int population);
        int EvictPopulationForShips();
        void SetPopulation(int population);
    }
}
