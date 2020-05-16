using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.Structs
{
    [Serializable]
    public struct CubicRange
    {
        [SerializeField] private Range _X_Range;
        [SerializeField] private Range _Y_Range;
        [SerializeField] private Range _Z_Range;

        public Range x {
            get => _X_Range;
            set => _X_Range = value;
        }
        public Range y {
            get => _Y_Range;
            set => _Y_Range = value;
        }
        public Range z {
            get => _Z_Range;
            set => _Z_Range = value;
        }

        public Vector3 min => new Vector3(_X_Range.min, _Y_Range.min, _Z_Range.min);
        public Vector3 max => new Vector3(_X_Range.max, _Y_Range.max, _Z_Range.max);
        public Vector3 mid => new Vector3(_X_Range.mid, _Y_Range.mid, _Z_Range.mid);
        public Vector3 random => new Vector3(_X_Range.random, _Y_Range.random, _Z_Range.random);
        public float volume => _X_Range.length * _Y_Range.length * _Z_Range.length;


        public Vector3[] corners {
            get {
                var counter = 0;
                var x = _X_Range.corners;
                var y = _Y_Range.corners;
                var z = _Z_Range.corners;
                var result = new Vector3[x.Length + y.Length + z.Length];

                for (var i = 0; i < x.Length; i++)
                    for (var j = 0; j < y.Length; j++)
                        for (var k = 0; k < z.Length; k++)
                            result[counter++] = new Vector3(x[i], y[j], z[k]);

                return result;
            }
        }

        /////////////////////////////////////////////////////////////

        public CubicRange(Range range) : this(range, range, range)
        { }

        public CubicRange(Range x, Range y, Range z)
        {
            _X_Range = x;
            _Y_Range = y;
            _Z_Range = z;
        }

        /////////////////////////////////////////////////////////////

        public bool Contains(Vector3 value)
            => x.Contains(value.x) && y.Contains(value.y) && z.Contains(value.z);

        public bool Contains(CubicRange range)
            => x.Contains(range.x) && y.Contains(range.y) && z.Contains(range.z);

        public bool Intersects(CubicRange range)
        {
            var my = this;

            if (my.corners.Any(c => range.Contains(c)))
                return true;

            if (range.corners.Any(c => my.Contains(c)))
                return true;

            return false;
        }

        #region CONVERSIONS
        public static implicit operator SquareRange(CubicRange range)
            => new SquareRange(range.x, range.y);
        #endregion // CONVERSIONS
    }
}
