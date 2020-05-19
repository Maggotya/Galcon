using Galcon.Level.Planets;
using UnityEngine.Events;

namespace Galcon.Level.Finishing
{
    public interface ILevelFinishedManager
    {
        UnityAction<string> onLevelFinished { get; set; }

        void CheckLevelFinished(IPlanet[] planets);
    }
}
