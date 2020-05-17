using UnityEngine;

namespace Core.Structs.Figures
{
    public struct Circle : IFigure
    {
        private Vector2 _point;
        private float _radius;

        public Vector2 point {
            get => _point;
            set => _point = value;
        }
        public float radius {
            get => _radius;
            set => _radius = value;
        }

        ///////////////////////////////////////////////
        
        public Circle(Vector2 point, float radius)
        {
            _point = point;
            _radius = radius;
        }
        public Circle(Vector2 point) : this(point, 0)
        { }
        public Circle(float radius) : this(Vector2.zero, radius)
        { }

        ///////////////////////////////////////////////

        public bool Contains(Vector2 point)
            => Vector2.SqrMagnitude(_point - point) <= Mathf.Pow(_radius, 2);
        public float DistanceFromBorder(Vector2 point)
            => DistanceFromCenter(point) - _radius;
        public float DistanceFromCenter(Vector2 point)
            => Vector2.Distance(_point, point);

        public Vector2 GetRandomPointInside()
        {
            var length = Random.Range(0f, _radius);
            var vector = Random.insideUnitCircle * length;

            return _point + vector;
        }

        public Vector2 GetRandomPointInDistance(float distance)
        {
            var length = Random.Range(_radius, _radius + distance);
            var vector = Random.insideUnitCircle * length;

            return _point + vector;
        }
    }
}
