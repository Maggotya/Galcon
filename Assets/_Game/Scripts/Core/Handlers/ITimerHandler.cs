using System;
using Core.Interfaces;

namespace Core.Handlers
{
    public interface ITimerHandler : ILaunchable, IUpdatable
    {
        float timerCapacity { get; }
        bool repeatable { get; set; }

        Action onCircleFinished { get; set; }
        Action onLaunched { get; set; }
        Action onStopped { get; set; }
        Action onContinued { get; set; }
        Action onPaused { get; set; }
    }
}
