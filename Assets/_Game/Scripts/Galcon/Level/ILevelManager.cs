using Core.Interfaces;
using Galcon.Level.PlayerManagement;
using UnityEngine.Events;

namespace Galcon.Level
{
    public interface  ILevelManager : IGameObjectHost
    {
        string status { get; }
        UnityEvent onLevelStarted { get; set; }
        UnityEvent onLevelFinished { get; set; }

        void StartLevel();
        void ClearLevel();
    }
}
