using System;
using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Core.Modules.ConvexHull;

namespace Galcon.Level.PlayerManagement
{
    public class ExtremeElementsGetter<T> : IExtremeElementsGetter<T> where T : IGameObjectHost
    {
        public T GetMostLeft(IEnumerable<T> items)
            => GetMostHorizontal(
                items,
                item => item.gameObject.transform.position.x,
                (c1, c2) => c1 < c2,
                GetMostDown);

        public T GetMostRight(IEnumerable<T> items)
            => GetMostHorizontal(
                items,
                item => item.gameObject.transform.position.x,
                (c1, c2) => c1 > c2,
                GetMostUp);

        public T GetMostUp(IEnumerable<T> items) => GetMostVertical(
                items,
                item => item.gameObject.transform.position.y,
                (c1, c2) => c1 >= c2);

        public T GetMostDown(IEnumerable<T> items)
            => GetMostVertical(
                items,
                item => item.gameObject.transform.position.y,
                (c1, c2) => c1 <= c2);

        //////////////////////////////////////////////////////////////////
        
        private T GetMostVertical(IEnumerable<T> items, Func<T, float> coordinate, Func<float, float, bool> condition)
        {
            if (items.Any() == false)
                return default;

            var result = items.First();

            foreach (var item in items)
                if (condition(coordinate(item), coordinate(result)))
                    result = item;

            return result;
        }

        private T GetMostHorizontal(IEnumerable<T> items, Func<T, float> coordinate, Func<float, float, bool> condition, Func<T[], T> result)
        {
            if (items.Any() == false)
                return default;

            var results = new Queue<T>();
            results.Enqueue(items.First());

            foreach (var item in items) {
                if (coordinate(item) == coordinate(results.Peek()))
                    results.Enqueue(item);

                if (condition(coordinate(item), coordinate(results.Peek()))) {
                    results.Clear();
                    results.Enqueue(item);
                }
            }

            return result(results.ToArray());
        }
    }
}
