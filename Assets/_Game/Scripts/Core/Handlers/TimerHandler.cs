using System;
using UnityEngine;

namespace Core.Handlers
{
    public class TimerHandler : ITimerHandler
    {
        private float _counter { get; set; }

        public float timerCapacity { get; private set; }
        public bool repeatable { get; set; }
        public bool launched { get; private set; }
        public bool paused { get; private set; }

        public Action onCircleFinished { get; set; }
        public Action onLaunched { get; set; }
        public Action onStopped { get; set; }
        public Action onContinued { get; set; }
        public Action onPaused { get; set; }

        ////////////////////////////////////////////////////////////

        public TimerHandler()
        {
            ResetCounter();

            repeatable = false;
            launched = false;
            paused = false;

            timerCapacity = Mathf.Infinity;
        }

        public TimerHandler(float timerCapacity) : this()
            => this.timerCapacity = timerCapacity;

        public TimerHandler(float timerCapacity, bool repeatable) : this(timerCapacity)
            => this.repeatable = repeatable;

        ////////////////////////////////////////////////////////////

        #region LAUNCHINGS
        public void Launch() 
        {
            if (launched && !paused)
                return;

            if (paused) {
                Continue();
                return;
            }

            SetLaunched(true);
            onLaunched?.Invoke();
        }
        public void Pause()
        {
            if (launched == false || paused)
                return;

            SetPaused(true);
            onPaused?.Invoke();
        }

        public void Stop()
        {
            if (launched == false)
                return;

            SetLaunched(false);
            SetPaused(false);
            onStopped?.Invoke();
        }

        private void Continue()
        {
            SetPaused(false);
            onContinued?.Invoke();
        }
        #endregion // LAUNCHINGS

        #region CIRCLES
        public void Update(float deltaTime)
        {
            if (launched == false || paused)
                return;

            if (IterateCounter(deltaTime))
                FinishCircle();
        }

        private void FinishCircle()
        {
            onCircleFinished?.Invoke();
            ResetCounter();

            if (repeatable == false)
                SetLaunched(false);
        }

        private bool IterateCounter(float deltaTime)
            => ( _counter += deltaTime ) >= timerCapacity;

        private void ResetCounter()
            => _counter = 0;
        #endregion // CIRCLES

        #region PRIVATE_SETTERS
        private void SetLaunched(bool status)
        {
            if (launched == status)
                return;
            launched = status;
        }

        private void SetPaused(bool status)
        {
            if (paused == status)
                return;
            paused = status;
        }
        #endregion // PRIVATE_SETTERS
    }
}
