using Galcon.Level.ScreenView;
using UnityEngine;

namespace Galcon.Level.ScreenView
{
    public class BorderDistanceCalculator : IBorderDistanceCalculator
    {
        public IBorder border { get; private set; }

        //////////////////////////////////////

        public BorderDistanceCalculator(IBorder border)
            => this.border = border;

        //////////////////////////////////////

        public float Calculate(Vector2 point)
        {
            if (border == null)
                return 0f;

            if (border.type == BorderType.Top || border.type == BorderType.Bottom)
                return Mathf.Abs(point.y - border.position.y);

            if (border.type == BorderType.Left || border.type == BorderType.Right)
                return Mathf.Abs(point.x - border.position.x);

            return 0;
        }
    }
}
