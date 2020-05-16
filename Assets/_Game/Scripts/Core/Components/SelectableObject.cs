using Core.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Components
{
    public class SelectableObject : ValueStateCheckableMonoBehaviour
    {
        [SerializeField] private bool _Selected;

        [Header("Events")]
        [SerializeField] private UnityEvent _OnSelected;
        [SerializeField] private UnityEvent _OnDeselected;
        [SerializeField] private BoolUnityEvent _OnChangeSelected;

        private bool _lastState;

        public bool selected => _Selected;

        public UnityEvent onSelected {
            get => _OnSelected;
            set => _OnSelected = value;
        }
        public UnityEvent onDeselected {
            get => _OnDeselected;
            set => _OnDeselected = value;
        }
        public BoolUnityEvent onChangeSelected {
            get => _OnChangeSelected;
            set => _OnChangeSelected = value;
        }

        ////////////////////////////////////////////

        protected override void InitState() => _lastState = _Selected;
        protected override bool HasStateChanged() => _lastState != _Selected;
        protected override void UpdateState() => SetSelected(_Selected);

        ////////////////////////////////////////////

        public void Select()
            => SetSelected(true);

        public void Deselect()
            => SetSelected(false);

        public void SetSelected(bool status)
        {
            if (_lastState == status)
                return;

            _Selected = status;
            _lastState = status;

            if (status) _OnSelected?.Invoke();
            else _OnDeselected?.Invoke();

            _OnChangeSelected?.Invoke(status);
            Logging.Log(_source, status ? "Selected" : "Deselected");
        }
    }
}
