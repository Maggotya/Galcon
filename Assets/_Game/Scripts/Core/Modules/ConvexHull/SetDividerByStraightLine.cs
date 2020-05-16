using System.Collections.Generic;
using Core.Interfaces;
using UnityEngine;

namespace Core.Modules.ConvexHull
{
    public class SetDividerByStraightLine<T> : ISetDivider<T> where T : IGameObjectHost
    {
        public Vector2 startPoint { get; set; }
        public Vector2 endPoint { get; set; }

        ////////////////////////////////////////////

        public SetDividerByStraightLine()
        { }

        public SetDividerByStraightLine(Vector2 startPoint, Vector2 endPoint)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
        }

        ////////////////////////////////////////////

        public HashSet<T>[] Divide(T[] items)
            => Divide(new HashSet<T>(items));

        public HashSet<T>[] Divide(HashSet<T> set)
        {
            var result = InitResultSet();

            foreach(var item in set) {
                var resultIndex = ResultIndexForItem(item, startPoint, endPoint);
                result[resultIndex].Add(item);
            }

            return result;
        }

        ///////////////////////////////////////////////////////

        private HashSet<T>[] InitResultSet()
        {
            var size = 2;
            var result = new HashSet<T>[size];

            for (var i = 0; i < size; i++)
                result[i] = new HashSet<T>();

            return result;
        }

        private int ResultIndexForItem(T item, Vector2 startPoint, Vector2 endPoint)
        {
            var straightLine = Line(startPoint, endPoint);
            var toPointLine = Line(startPoint, item.gameObject.transform.position);

            return PseudoCross(straightLine, toPointLine) >= 0 ? 0 : 1;
        }

        private Vector2 Line(Vector2 start, Vector2 end)
            => end - start;

        private float PseudoCross(Vector2 a, Vector2 b)
            => Vector3.Cross(a, b).magnitude;
    }
}
