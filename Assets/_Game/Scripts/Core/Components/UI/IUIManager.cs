using UnityEngine.Events;

namespace Core.Components.UI
{
    public interface IUIManager
    {
        UnityEvent onFirstScreenOpened { get; set; }
        UnityEvent onLastScreenClosed { get; set; }
        UnityEvent onScreenOpened { get; set; }
        UnityEvent onScreenClosed { get; set; }

        void Open(ScreenType screenType);
        void CloseAll();
    }
}
