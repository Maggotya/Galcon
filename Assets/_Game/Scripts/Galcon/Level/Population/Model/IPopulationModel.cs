using System;
using Core.Interfaces;

namespace Galcon.Level.Population.Model
{
    interface IPopulationModel : IResetable
    {
        int count { get; }
        Action<int> onChanged { get; set; }
        Action onBecomePositive { get; set; }
        Action onBecomeZero { get; set; }

        void Set(int count);
        void Increase(int count);
        void Decrease(int count);
    }
}
