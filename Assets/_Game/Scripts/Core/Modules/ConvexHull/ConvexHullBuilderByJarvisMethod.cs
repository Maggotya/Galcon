using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Galcon.Level.PlayerManagement;
using UnityEngine;

namespace Core.Modules.ConvexHull
{
    public class ConvexHullBuilderByJarvisMethod<T> : IConvexHullBuilder<T> where T : IGameObjectHost
    {
        private IExtremeElementsGetter<T> _extremeElements;

        ///////////////////////////////////////////////////////////////

        public ConvexHullBuilderByJarvisMethod()
        {
            _extremeElements = new ExtremeElementsGetter<T>();
        }

        ///////////////////////////////////////////////////////////////

        public T[] Build(IEnumerable<T> items)
        {
            var length = items.Count();

            if (length < 4)
                return items.ToArray();

            var result = new HashSet<T>();
            var counter = 0;
            var counterBound = length;

            var startItem = _extremeElements.GetMostLeft(items);

            var item1 = startItem;
            var item2 = default(T);
            var point1 = (Vector2)item1.gameObject.transform.position;
            var point2 = point1 + Vector2.right;

            item2 = GetMostRightItem(point1, point2, items, item1);
            point2 = item2.gameObject.transform.position;

            result.Add(item1);
            result.Add(item2);

            while(counter++ < counterBound && item2.gameObject != startItem.gameObject) {
                var nextItem = GetMostRightItem(point1, point2, items, item1, item2);
                item1 = item2;
                item2 = nextItem;

                point1 = point2;
                point2 = item2.gameObject.transform.position;

                result.Add(item2);
            }

            return result.ToArray();
        }

        ///////////////////////////////////////////////////////////////

        private T GetMostRightItem(Vector2 startPoint, Vector2 endPoint, IEnumerable<T> items, params T[] exceptions)
        {
            var minAngle = Mathf.Infinity;
            var rightItem = default(T);
            var baseVector = endPoint - startPoint;

            foreach (var item in items.Except(exceptions)) {
                var vec = (Vector2)item.gameObject.transform.position - startPoint;
                var angle = Vector3.Cross(baseVector.normalized, vec.normalized).z;

                if (angle < minAngle) {
                    minAngle = angle;
                    rightItem = item;
                }
            }

            return rightItem;
        }

    }
}
