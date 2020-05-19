using System.Linq;
using Galcon.Level.Planets;
using UnityEngine.Events;

namespace Galcon.Level.Finishing
{
    public class LevelFinishedManager : ILevelFinishedManager
    {
        public UnityAction<string> onLevelFinished { get; set; }

        ///////////////////////////////////////////////////////////

        public LevelFinishedManager()
        { }

        ///////////////////////////////////////////////////////////

        public void CheckLevelFinished(IPlanet[] planets)
        {
            var owners = planets.Where(p => !p.owner.isClear).Select(p => p.owner.ownerTag).Distinct();
            var count = owners.Count();

            if (count > 1)
                return;

            var winner = count > 0 ? owners.First() : "";
            onLevelFinished?.Invoke(winner);
        }
    }
}
