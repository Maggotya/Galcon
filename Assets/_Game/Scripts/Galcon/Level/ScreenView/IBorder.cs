﻿using UnityEngine;

namespace Galcon.Level.ScreenView
{
    interface IBorder
    {
        BorderType type { get; }
        Vector2 position { get; }
        Vector2 size { get; }

        float GetDistance(Vector2 point);
    }
}
