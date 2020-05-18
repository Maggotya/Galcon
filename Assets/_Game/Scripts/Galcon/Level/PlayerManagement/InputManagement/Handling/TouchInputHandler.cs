using UnityEngine;

namespace Galcon.Level.PlayerManagement.InputManagement.Handling
{
    public class TouchInputHandler : InputHandler
    {
        protected override bool _canHandle => Input.touchSupported;

        /////////////////////////////////////////////////////////
        
        protected override void OnHandle()
        {
            foreach (var touch in Input.touches) {
                if (touch.phase == TouchPhase.Began)
                    onInputBegan?.Invoke(touch.position);

                if (touch.phase == TouchPhase.Stationary)
                    onInputStationary?.Invoke(touch.position);

                if (touch.phase == TouchPhase.Moved)
                    onInputMoved?.Invoke(touch.position);

                if (touch.phase == TouchPhase.Ended)
                    onInputEnded?.Invoke(touch.position);
            }
        }
    }
}
