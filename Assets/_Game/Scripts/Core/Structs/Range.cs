using System;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Core.Structs
{
    [Serializable]
    public struct Range
    {
        [SerializeField] private float _Min;
        [SerializeField] private float _Max;

        public float min {
            get => _Min;
            set {
                _Min = value;
                CorrectBorders();
            }
        }
        public float max {
            get => _Max;
            set {
                _Max = value;
                CorrectBorders();
            }
        }

        public float mid => ( min + max ) / 2f;
        public float length => Mathf.Abs(max - min);
        public float random => Random.Range(_Min, _Max);
        public float[] corners => new float[2] { min, max };

        /////////////////////////////////////////////////////////////

        public Range(float value) : this(value, value)
        { }

        public Range(float min, float max)
        {
            _Min = min;
            _Max = max;
        }

        /////////////////////////////////////////////////////////////

        public bool Contains(float value)
            => min <= value && value <= max;

        public bool Contains(Range range)
            => min <= range.min && range.max <= max;

        public bool Intersects(Range range)
            => Contains(range.min) || Contains(range.max)
            || range.Contains(min) || range.Contains(max);

        /////////////////////////////////////////////////////////////

        private void CorrectBorders()
        {
            if (_Min <= _Max)
                return;

            var buf = _Min;
            _Min = _Max;
            _Max = buf;
        }
    }
}
