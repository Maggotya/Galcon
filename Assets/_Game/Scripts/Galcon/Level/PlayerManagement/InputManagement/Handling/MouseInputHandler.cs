using UnityEngine;
using UnityEngine.Events;

namespace Galcon.Level.PlayerManagement.InputManagement.Handling
{
    public class MouseInputHandler : InputHandler
    {
        protected override bool _canHandle => true;

        private Vector2 _lastMousePosition { get; set; }
        private Vector2 _currentPosition => Input.mousePosition;
        private bool _isMouseMoved => _lastMousePosition != _currentPosition;

        /////////////////////////////////////////////////////////

        public MouseInputHandler() : base()
        { }

        public MouseInputHandler(IInputHandler successor) : base(successor)
        { }

        /////////////////////////////////////////////////////////

        protected override void OnHandle()
        {
            if (Input.GetMouseButtonDown(0))
                OnMouseBegan();

            if (Input.GetMouseButton(0))
                if (_isMouseMoved) OnMouseStationary();
                else OnMouseMoved();

            if (Input.GetMouseButtonUp(0))
                OnMouseEnded();
        }

        /////////////////////////////////////////////////////////

        private void OnMouseBegan()
            => OnEvent(onInputBegan);

        private void OnMouseMoved() 
            => OnEvent(onInputMoved);

        private void OnMouseStationary()
            => OnEvent(onInputStationary);

        private void OnMouseEnded()
            => OnEvent(onInputEnded);

        private void OnEvent(UnityAction<Vector2> action)
        {
            UpdateLastPosition();
            action?.Invoke(_currentPosition);
        }

        private void UpdateLastPosition()
            => _lastMousePosition = Input.mousePosition;
    }
}
