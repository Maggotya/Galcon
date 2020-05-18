using Core.Extensions;

namespace Galcon.Level.PlayerManagement.InputManagement
{
    public interface  IInputManager
    {
        Vector2UnityEvent onInputBegan { get; set; }
        Vector2UnityEvent onInputMoved { get; set; }
        Vector2UnityEvent onInputStationary { get; set; }
        Vector2UnityEvent onInputEnded { get; set; }
    }
}
