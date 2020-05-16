
using System.Collections.Generic;
using Core.Interfaces;

namespace Core.Modules.ConvexHull
{
    public interface IExtremeElementsGetter<T> where T : IGameObjectHost
    {
        T GetMostLeft(IEnumerable<T> items);
        T GetMostRight(IEnumerable<T> items);
        T GetMostUp(IEnumerable<T> items);
        T GetMostDown(IEnumerable<T> items);
    }
}
