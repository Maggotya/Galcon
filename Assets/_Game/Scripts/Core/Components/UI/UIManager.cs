using System.Linq;
using Core.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Components.UI
{
    public class UIManager : MyMonoBehaviour, IUIManager
    {
        [SerializeField] private UnityEvent _OnFirstScreenOpened;
        [SerializeField] private UnityEvent _OnLastScreenClosed;
        [SerializeField] private UnityEvent _OnScreenOpened;
        [SerializeField] private UnityEvent _OnScreenClosed;

        private IScreen[] _screens { get; set; }
            = new IScreen[0];

        public UnityEvent onFirstScreenOpened {
            get => _OnFirstScreenOpened;
            set => _OnFirstScreenOpened = value;
        }
        public UnityEvent onLastScreenClosed {
            get => _OnLastScreenClosed;
            set => _OnLastScreenClosed = value;
        }
        public UnityEvent onScreenOpened {
            get => _OnScreenOpened;
            set => _OnScreenOpened = value;
        }
        public UnityEvent onScreenClosed {
            get => _OnScreenClosed;
            set => _OnScreenClosed = value;
        }
        //////////////////////////////////////////////////////

        private void OnEnable()
            => Initialize();

        //////////////////////////////////////////////////////

        private void Initialize()
        {
            _screens = GetComponentsInChildren<IScreen>(true);

            foreach (var screen in _screens) {
                screen.onOpened.AddListener(() => OnScreenOpened(screen));
                screen.onClosed.AddListener(() => OnScreenClosed(screen));
            }
        }

        //////////////////////////////////////////////////////

        private void OnScreenOpened(IScreen screen)
        {
            if (screen == null)
                return;

            if (screen.countedAsFirst && _screens.All(s => !s.opened || s == screen || !s.countedAsFirst))
                _OnFirstScreenOpened?.Invoke();

            _OnScreenOpened?.Invoke();
            CloseExcept(screen);
        }

        private void OnScreenClosed(IScreen screen)
        {
            if (screen == null)
                return;

            if (screen.countedAsLast && _screens.All(s => !s.opened || s == screen || !s.countedAsLast))
                _OnLastScreenClosed?.Invoke();

            _OnScreenClosed?.Invoke();
        }

        //////////////////////////////////////////////////////

        public IScreen Open(ScreenType screenType)
        {
            var screen = GetScreen(screenType);
            screen?.Open();

            return screen;
        }

        [ContextMenu("Close All")]
        public void CloseAll()
            => CloseExcept();

        //////////////////////////////////////////////////////

        private IScreen GetScreen(ScreenType screenType)
        {
            var result = _screens.FirstOrDefault(s => s.type == screenType);

            if (result == null)
                Logging.Error(_source, $"Can' find screen of type {screenType:F}");

            return result;
        }

        private void CloseExcept(params IScreen[] exceptions)
        {
            foreach (var screen in _screens.Except(exceptions).ToArray())
                screen.Close();
        }
    }
}
