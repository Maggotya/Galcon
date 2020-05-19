using Core;
using Core.Extensions;
using UnityEngine;

namespace Core.Components.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIBackground : ValueStateCheckableMonoBehaviour
    {
        [SerializeField] private bool _Visible;

        private bool _lastState { get; set; }

        public bool visible => _Visible;

        /////////////////////////////////////////////////

        #region STATE_CHECKING
        protected override void InitState() => _lastState = !_Visible;
        protected override bool HasStateChanged() => _lastState != _Visible;
        protected override void UpdateState() => SetVisible(_Visible);
        #endregion // STATE_CHECKING

        [ContextMenu("Show")]
        public void Show()
            => SetVisible(true);

        [ContextMenu("Hide")]
        public void Hide()
            => SetVisible(false);

        /////////////////////////////////////////////////

        private void SetVisible(bool status)
        {
            if (_lastState == status)
                return;

            _lastState = status;
            _Visible = status;
            gameObject.SetActive(status);
            Logging.Log(_source, status ? "Showed" : "Hided");
        }
    }
}
