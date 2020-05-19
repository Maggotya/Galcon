using Core.Components.UI.Views;
using Core.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Components.UI.Screens
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ScreenController : ValueStateCheckableMonoBehaviour, IScreen
    {
        #region SERIALIZE_FIELDS

        [Header("Settings")]
        [SerializeField] private ScreenType _Type;
        [SerializeField] private bool _CountedAsFirst = true;
        [SerializeField] private bool _CountedAsLast = true;

        [Header("State")]
        [SerializeField] private bool _Opened;

        [Header("Events")]
        [SerializeField] private UnityEvent _OnOpened;
        [SerializeField] private UnityEvent _OnClosed;

        #endregion // SERIALIZE_FIELDS

        #region PRIVATE_FIELDS

        private IView _view_Cache;
        private IView _view {
            get {
                if (_view_Cache == null) {
                    _view_Cache = new ViewByDoTween(GetComponent<CanvasGroup>());
                    _view_Cache.onPlayOpenStarted += () => gameObject.SetActive(true);
                    _view_Cache.onPlayCloseFinished += () => gameObject.SetActive(false);
                }
                return _view_Cache;
            }
        }

        private bool _lastState { get; set; }
        protected override string _source => $"{_Type:F}_Screen";

        #endregion // PRIVATE_FIELDS

        #region PUBLIC_FIELDS
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

        #endregion // PUBLIC_FIELDS

        /////////////////////////////////////////////////

        #region STATE_CHECKING
        protected override void InitState() => _lastState = !_Opened;
        protected override bool HasStateChanged() => _lastState != _Opened;
        protected override void UpdateState() => SetOpened(_Opened);
        #endregion // STATE_CHECKING

        /////////////////////////////////////////////////

        #region PUBLIC_METHODS

        [ContextMenu("Open")]
        public void Open()
            => SetOpened(true);

        [ContextMenu("Close")]
        public void Close() 
            => SetOpened(false);

        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS
        private void SetOpened(bool status)
        {
            if (_lastState == status)
                return;

            _Opened = status;
            _lastState = status;

            if (_Opened) _OnOpened?.Invoke();
            else _OnClosed?.Invoke();

            if (_Opened) _view.PlayOpen();
            else _view.PlayClose();

            Logging.Log(_source, status ? "Opened" : "Closed");
        }

        #endregion // PRIVATE_METHODS
    }
}
