using System;
using System.Collections;
using Core.Extensions;
using Core.Interfaces;
using UnityEngine;

namespace Core.Components
{
    [RequireComponent(typeof(IDestroyable))]
    public class AutoDestroyable : MyMonoBehaviour
    {
        [SerializeField] private bool _AutoStart;
        [SerializeField] private float _Timer;

        private IDestroyable _destroyable;
        private Coroutine _timerActions { get; set; }

        public bool started => _timerActions != null;

        ///////////////////////////////////////////////
        
        private void OnEnable()
        {
            Initialize();

            if (_AutoStart)
                StartTimer();
        }

        private void OnDisable()
            => StopTimer();

        ///////////////////////////////////////////////

        public void StartTimer()
        {
            StopTimer();

            _timerActions = StartCoroutine(TimerActions(
                () => _timerActions = null));

            Logging.Log(_source, $"Started for {_Timer} seconds");
        }

        public void StopTimer()
        {
            if (_timerActions == null)
                return;

            StopCoroutine(_timerActions);
            _timerActions = null;
            Logging.Log(_source, "Stopped");
        }

        ///////////////////////////////////////////////

        private void Initialize()
            => Attach(ref _destroyable);

        private IEnumerator TimerActions(Action callback = null)
        {
            yield return new WaitForSeconds(_Timer);
            DestroyDestroyable();
            callback?.Invoke();
        }

        private void DestroyDestroyable()
        {
            _destroyable?.Destroy();
            Logging.Log(_source, "Destroy");
        }
    }
}
