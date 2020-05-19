using UnityEngine;
using UnityEngine.Events;

namespace Core.Tweaks
{
    public class OnEnableActions : MonoBehaviour
    {
        [SerializeField] public UnityEvent onEnableEvent;

        ////////////////////////////////////////////////

        private void OnEnable()
            => onEnableEvent?.Invoke();
    }
}