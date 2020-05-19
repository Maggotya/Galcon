using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Components.UI.Views
{
    class ViewByDoTween : IView
    {
        public UnityAction onPlayOpenStarted { get; set; }
        public UnityAction onPlayOpenFinished { get; set; }
        public UnityAction onPlayCloseStarted { get; set; }
        public UnityAction onPlayCloseFinished { get; set; }

        private readonly CanvasGroup _canvasGroup;

        private Tween _tween { get; set; }

        private const float _DISABLE_ALPHA = 0F;
        private const float _ENABLE_ALPHA = 1F;
        private const float _DURATION = 0.5f;

        //////////////////////////////////////////////////////

        public ViewByDoTween(CanvasGroup canvasGroup)
            => _canvasGroup = canvasGroup;

        //////////////////////////////////////////////////////

        public void PlayOpen()
            => Play(true, onPlayOpenStarted, onPlayOpenFinished);

        public void PlayClose()
            => Play(false,onPlayCloseStarted,  onPlayCloseFinished);

        //////////////////////////////////////////////////////

        private void Play(bool status, UnityAction startCallback, UnityAction endCallback)
        {
            _tween?.Kill();

            startCallback?.Invoke();

            if (_canvasGroup)
                _tween = CreateTween(_canvasGroup, status)
                    .OnComplete(() => endCallback?.Invoke());
            else
                endCallback?.Invoke();
        }

        private Tween CreateTween(CanvasGroup canvasGroup, bool status)
            => DOTween.To(
                a => canvasGroup.alpha = a,
                status ? _DISABLE_ALPHA : _ENABLE_ALPHA,
                status ? _ENABLE_ALPHA : _DISABLE_ALPHA,
                _DURATION).SetUpdate(true);
    }
}
