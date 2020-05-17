using Core.Extensions;
using UnityEngine;

namespace Core.Components
{
    public class AutoRotateToMoving2D : MyMonoBehaviour
    {
        private Transform _transform;

        private Vector2 _lastPosition { get; set; }
        private Vector2 _currentPosition => _transform.position;

        //////////////////////////////////////////////////

        private void OnEnable()
        {
            Attach(ref _transform);
            UpdateLastPosition();
        }

        private void LateUpdate()
        {
            if (_lastPosition != _currentPosition) {
                UpdateRotation(_lastPosition);
                UpdateLastPosition();
            }
        }

        //////////////////////////////////////////////////

        private void UpdateLastPosition()
            => _lastPosition = _currentPosition;

        private void UpdateRotation(Vector2 lastPosition)
        {
            var deltaPosition = _currentPosition - lastPosition;

            if (deltaPosition != Vector2.zero)
                _transform.up = deltaPosition.normalized;
        }
    }
}
