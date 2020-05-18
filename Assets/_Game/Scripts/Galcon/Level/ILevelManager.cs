using Core.Interfaces;

namespace Galcon.Level
{
    public interface  ILevelManager : IGameObjectHost
    {
        void StartLevel();
        void ClearLevel();
    }
}
