using Core.Interfaces;

namespace Core.Modules
{
    public interface ITimeScaleManager : IResetable
    {
        void Start();
        void Stop();
        void EnableScale(bool status);
    }
}
