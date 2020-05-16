using UnityEngine;

namespace Core.Handlers
{
    public class SpeedHandler : ISpeedHandler
    {
        public float start { get; set; }
        public float max { get; set; }
        public float acceleration { get; set; }

        public float current { get; private set; }
        public bool launched { get; private set; }
        public bool paused { get; private set; }

        private const string _SOURCE = "SpeedController";

        //////////////////////////////////////////////////

        #region CONSTRUCTORS
        public SpeedHandler()
        {
            start = 0f;
            acceleration = 0f;
            max = Mathf.Infinity;

            launched = false;
            paused = false;

            current = start;
        }

        public SpeedHandler(float speed) : this()
            => start = speed;
        public SpeedHandler(float startSpeed, float acceleration) : this(startSpeed)
            => this.acceleration = acceleration;
        public SpeedHandler(float startSpeed, float acceleration, float maxSpeed) : this(startSpeed, acceleration)
            => this.max = maxSpeed;
        #endregion // CONSTRUCTORS

        //////////////////////////////////////////////////

        #region LAUNCHING
        public void Launch()
        {
            if (launched && !paused)
                return;

            SetPaused(false);
            SetLaunched(true);
        }

        public void Pause()
        {
            if (launched == false)
                return;

            SetPaused(true);
        }

        public void Stop()
        {
            if (launched == false)
                return;

            ResetSpeed();
            SetPaused(false);
            SetLaunched(false);
        }

        //////////////////////////////////////////////////

        private void SetPaused(bool status)
        {
            if (paused == status)
                return;

            paused = status;
            Logging.Log(_SOURCE, status ? "Pauses" : "Unpaused");
        }

        private void SetLaunched(bool status)
        {
            if (launched == status)
                return;

            launched = status;
            Logging.Log(_SOURCE, status ? "Launched" : "Unpaused");
        }
        #endregion // LAUNCHING

        #region SPEED_UPDATING
        public void IncreaseSpeed(float deltaTime)
            => ChangeSpeed(deltaTime);

        public void DicreaseSpeed(float deltaTime)
            => ChangeSpeed(-deltaTime);

        //////////////////////////////////////////////////

        private void ChangeSpeed(float delatTime)
        {
            if (paused || launched == false)
                return;

            current += acceleration * delatTime;
            current = Mathf.Clamp(current, start, max);
        }

        private void ResetSpeed()
            => current = start;
        #endregion // SPEED_UPDATING
    }
}
