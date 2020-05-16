
using System.Collections.Generic;
using Core.Interfaces;

namespace Core.Modules.ConvexHull
{
    public interface IConvexHullBuilder<T> where T : IGameObjectHost
    {
        T[] Build(IEnumerable<T> items);
    }
}
