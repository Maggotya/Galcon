using System.Collections.Generic;
using Core.Interfaces;

namespace Core.Modules.ConvexHull
{
    public interface ISetDivider<T> where T : IGameObjectHost
    {
        HashSet<T>[] Divide(T[] items);
        HashSet<T>[] Divide(HashSet<T> set);
    }
}
