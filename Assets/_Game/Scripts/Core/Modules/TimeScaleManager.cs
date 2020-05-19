using UnityEngine;

namespace Core.Modules
{
    public class TimeScaleManager : ITimeScaleManager
    {
        private const int STOPPED_SCALE = 0;
        private const int ACTION_SCALE = 1;

        /////////////////////////////////////////////////////
        
        public void Start()
            => EnableScale(true);

        public void Stop()
            => EnableScale(false);

        public void EnableScale(bool status)
            => Time.timeScale = status ? ACTION_SCALE : STOPPED_SCALE;

        public void Reset()
            => Start();
    }
}
