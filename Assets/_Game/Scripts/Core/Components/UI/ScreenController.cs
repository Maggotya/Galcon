using Core.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Components.UI
{
    public class ScreenController : ValueStateCheckableMonoBehaviour, IScreen
    {
        [Header("Settings")]
        [SerializeField] private ScreenType _Type;
        [SerializeField] private bool _CountedAsFirst = true;
        [SerializeField] private bool _CountedAsLast = true;

        [Header("State")]
        [SerializeField] private bool _Opened;

        [Header("Events")]
        [SerializeField] private UnityEvent _OnOpened;
        [SerializeField] private UnityEvent _OnClosed;

        private bool _lastState { get; set; }
        protected override string _source => $"{_Type:F}_Screen";

        public ScreenType type => _Type;
        public bool countedAsFirst => _CountedAsFirst;
        public bool countedAsLast => _CountedAsLast;
        public bool opened => _Opened;

        public UnityEvent onOpened {
            get => _OnOpened;
            set => _OnOpened = value;
        }
        public UnityEvent onClosed {
            get => _OnClosed;
            set => _OnClosed = value;
        }
        /////////////////////////////////////////////////

        #region STATE_CHECKING
        protected override void InitState() => _lastState = !_Opened;
        protected override bool HasStateChanged() => _lastState != _Opened;
        protected override void UpdateState() => SetOpened(_Opened);
        #endregion // STATE_CHECKING

        /////////////////////////////////////////////////

        [ContextMenu("Open")]
        public void Open()
            => SetOpened(true);

        [ContextMenu("Close")]
        public void Close() 
            => SetOpened(false);

        /////////////////////////////////////////////////

        private void SetOpened(bool status)
        {
            if (_lastState == status)
                return;

            _Opened = status;
            _lastState = status;
            gameObject.SetActive(status);

            if (_Opened) _OnOpened?.Invoke();
            else _OnClosed?.Invoke();

            Logging.Log(_source, status ? "Opened" : "Closed");
        }
    }
}
