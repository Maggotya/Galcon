using Core.Structs;
using UnityEngine;

namespace Galcon.Level.ScreenView
{
    interface IBorderedArea
    {
        IBorder left { get; }
        IBorder right { get; }
        IBorder top { get; }
        IBorder bottom { get; }
        IBorder[] borders { get; }
        SquareRange area { get; }

        IBorder GetBorder(BorderType type);
        float[] GetDistanciesToBorders(Vector2 point);
        float GetMinDistanceToBorder(Vector2 point);
    }
}
