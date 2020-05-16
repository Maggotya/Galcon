using Core.Interfaces;

namespace Core.Handlers
{
    public interface ISpeedHandler : ILaunchable
    {
        float start { get; set; }
        float max { get; set; }
        float acceleration { get; set; }

        float current { get; }

        void IncreaseSpeed(float deltaTime);
        void DicreaseSpeed(float deltaTime);
    }
}
