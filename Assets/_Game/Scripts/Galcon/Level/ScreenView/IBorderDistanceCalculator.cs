using UnityEngine;

namespace Galcon.Level.ScreenView
{
    public interface  IBorderDistanceCalculator
    {
        float Calculate(Vector2 point);
    }
}
