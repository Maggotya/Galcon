using UnityEngine;

namespace Galcon.Level.ScreenView
{
    interface IBorderDistanceCalculator
    {
        float Calculate(Vector2 point);
    }
}
