using Core.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Components
{
    [RequireComponent(typeof(Renderer))]
    public class MaterialToggle : ValueStateCheckableMonoBehaviour
    {
        #region SERIALIZE_FIELDS
        [SerializeField] private bool _Toggle;

        [Header("Material Settings")]
        [SerializeField] private Material _ToggleOn;
        [SerializeField] private Material _ToggleOff;

        [Header("Events")]
        [SerializeField] private UnityEvent _OnToggleOn;
        [SerializeField] private UnityEvent _OnToggleOff;
        [SerializeField] private BoolUnityEvent _OnToggle;
        #endregion // SERIALIZE_FIELDS

        #region PRIVATE_FIELDS
        private Renderer _renderer { get; set; }
        private bool _lastState { get; set; }
        #endregion // PRIVATE_FIELDS

        #region PUBLIC_FIELDS
        public bool toggle {
            get => _Toggle;
            set => SetToggle(value);
        }

        public UnityEvent onToggleOn { 
            get => _OnToggleOn;
            set => _OnToggleOn = value;
        }
        public UnityEvent onToggleOff {
            get => _OnToggleOff;
            set => _OnToggleOff = value;
        }
        public BoolUnityEvent onToggle {
            get => _OnToggle;
            set => _OnToggle = value;
        }
        #endregion // PUBLIC_FIELDS

        ////////////////////////////////////////////////////

        #region INITIALIZATION
        private void OnEnable()
            => Initialize();

        private void Initialize()
        {
            if (_renderer == null)
                _renderer = GetComponent<Renderer>();
        }
        #endregion // INITIALIZATION

        #region STATE_CHECKING
        protected override void InitState() => _lastState = _Toggle;
        protected override bool HasStateChanged() => _lastState != _Toggle;
        protected override void UpdateState() => SetToggle(_Toggle);
        #endregion // STATE_CHECKING

        ////////////////////////////////////////////////////

        #region PUBLIC_METHODS
        public void SetToggle(bool state)
        {
            if (_lastState == state)
                return;

            _lastState = state;
            _Toggle = state;
            SetMaterial(state);

            if (state) _OnToggleOn?.Invoke();
            else _OnToggleOff?.Invoke();

            _OnToggle?.Invoke(state);
            Logging.Log(_source, state ? "Toggle On" : "Toggle Off");
        }
        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS
        private void SetMaterial(bool status)
        {
            var material = status ? _ToggleOn : _ToggleOff;
            _renderer.material = material;
        }
        #endregion // PRIVATE_METHODS
    }
}
