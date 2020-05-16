using UnityEngine;
using UnityEngine.Events;

namespace Galcon.Level.PlayerManagement.InputManagement.Handling
{
    interface IInputHandler
    {
        IInputHandler successor { get; set; }

        UnityAction<Vector2> onInputBegan { get; set; }
        UnityAction<Vector2> onInputMoved { get; set; }
        UnityAction<Vector2> onInputStationary { get; set; }
        UnityAction<Vector2> onInputEnded { get; set; }

        void Handle();
    }
}
