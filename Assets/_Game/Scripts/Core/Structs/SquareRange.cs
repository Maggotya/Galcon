using System;
using System.Linq;
using UnityEngine;

namespace Core.Structs
{
    [Serializable]
    public struct SquareRange
    {
        [SerializeField] private Range _X_Range;
        [SerializeField] private Range _Y_Range;

        public Range x {
            get => _X_Range;
            set => _X_Range = value;
        }
        public Range y {
            get => _Y_Range;
            set => _Y_Range = value;
        }

        public Vector2 min => new Vector2(_X_Range.min, _Y_Range.min);
        public Vector2 max => new Vector2(_X_Range.max, _Y_Range.max);
        public Vector2 mid => new Vector2(_X_Range.mid, _Y_Range.mid);
        public Vector2 random => new Vector2(_X_Range.random, _Y_Range.random);
        public float volume => _X_Range.length * _Y_Range.length;

        public Vector2[] corners {
            get {
                var counter = 0;
                var x = _X_Range.corners;
                var y = _Y_Range.corners;
                var result = new Vector2[x.Length + y.Length];

                for (var i = 0; i < x.Length; i++)
                    for (var j = 0; j < y.Length; j++)
                            result[counter++] = new Vector2(x[i], y[j]);

                return result;
            }
        }

        /////////////////////////////////////////////////////////////

        public SquareRange(Range range) : this(range, range)
        { }

        public SquareRange(Range x, Range y)
        {
            _X_Range = x;
            _Y_Range = y;
        }

        /////////////////////////////////////////////////////////////

        public bool Contains(Vector2 value)
            => x.Contains(value.x) && y.Contains(value.y);

        public bool Contains(SquareRange range)
            => x.Contains(range.x) && y.Contains(range.y);

        public bool Intersects(SquareRange range)
        {
            var my = this;

            if (my.corners.Any(c => range.Contains(c)))
                return true;

            if (range.corners.Any(c => my.Contains(c)))
                return true;

            return false;
        }

        #region CONVERSIONS
        public static implicit operator CubicRange(SquareRange range)
            => new CubicRange(range.x, range.y, new Range(0));
        #endregion // CONVERSIONS
    }
}
