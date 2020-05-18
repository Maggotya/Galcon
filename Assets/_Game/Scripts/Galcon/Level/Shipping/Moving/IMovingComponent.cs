using UnityEngine;
using UnityEngine.Events;

namespace Galcon.Level.Shipping.Moving
{
    public interface  IMovingComponent
    {
        bool moving { get; }

        UnityAction onStartMoving { get; set; }
        UnityAction onFinishMoving { get; set; }
        UnityAction<Vector2> onMoving { get; set; }
        UnityAction onStopped { get; set; }

        void MoveTo(Vector2 position);
        void Stop();
    }
}
