using UnityEngine;
using UnityEngine.Events;

namespace Galcon.Level.PlayerManagement.SelectionManagement
{
    public interface  ISelectionManager
    {
        IPlanetUnityEvent onPlanetSelected { get; set; }
        IPlanetUnityEvent onPlanetDeselected { get; set; }
        IPlanetUnityEvent onPlanetClicked { get; set; }
        UnityEvent onDeselectedAll { get; set; }

        void InputStarted(Vector2 screenPoint);
        void InputEnded(Vector2 screenPoint);
        void InputStationary(Vector2 screenPoint);
        void InputMoved(Vector2 screenPoint);
    }
}
