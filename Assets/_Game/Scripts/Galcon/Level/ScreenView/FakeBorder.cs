using System;
using UnityEngine;

namespace Galcon.Level.ScreenView
{
    public class FakeBorder : IBorder
    {
        private IBorderDistanceCalculator _distance { get; set; }

        public BorderType type { get; private set; }
        public Vector2 position { get; private set; }
        public Vector2 size { get; private set; }

        ///////////////////////////////////////////////

        public FakeBorder(BorderType type, Vector2 position, Vector2 size)
        {
            this.type = type;
            this.position = position;
            this.size = size;

            _distance = new BorderDistanceCalculator(this);
        }

        ///////////////////////////////////////////////

        public float GetDistance(Vector2 point)
            => _distance.Calculate(point);
    }
}
