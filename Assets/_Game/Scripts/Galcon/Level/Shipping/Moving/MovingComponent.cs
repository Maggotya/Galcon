using System.Collections;
using Core.Handlers;
using Core.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Galcon.Level.Shipping.Moving
{
    public class MovingComponent : IMovingComponent
    {
        private readonly MonoBehaviour _host;
        private readonly Transform _transform;
        private readonly ISpeedHandler _speedHandler;

        private Coroutine _movingCoroutine { get; set; }

        public bool moving { get; private set; }
        
        public UnityAction onStartMoving { get; set; }
        public UnityAction onFinishMoving { get; set; }
        public UnityAction<Vector2> onMoving { get; set; }
        public UnityAction onStopped { get; set; }

        /////////////////////////////////////////////////////////////////

        public MovingComponent(MonoBehaviour host, Transform transform, ISpeedConfigs speedConfigs)
        {
            _host = host;
            _transform = transform;

            _speedHandler = new SpeedHandler(speedConfigs.startSpeed, speedConfigs.acceleration, speedConfigs.acceleration);
            _speedHandler.Stop();
        }

        /////////////////////////////////////////////////////////////////

        public void MoveTo(Vector2 position)
        {
            moving = true;
            StopCoroutine();
            _movingCoroutine = _host.StartCoroutine(MovingCoroutine(position, OnMovingFinished));

            onStartMoving?.Invoke();
        }

        public void Stop()
        {
            if (moving == false)
                return;

            moving = false;
            StopCoroutine();
            onStopped?.Invoke();
        }

        /////////////////////////////////////////////////////////////////

        private void StopCoroutine()
        {
            if (_movingCoroutine == null)
                return;

            _host.StopCoroutine(_movingCoroutine);
            _movingCoroutine = null;
        }
        
        private void OnMovingFinished()
        {
            moving = false;
            StopCoroutine();
            onFinishMoving?.Invoke();
        }

        private IEnumerator MovingCoroutine(Vector2 targetPoint, UnityAction callback)
        {
            _speedHandler.Launch();
            var deltaPosition = Vector2.zero;

            while(!IsArrived(targetPoint, deltaPosition)) {

                deltaPosition = CalculateDeltaPosition(_speedHandler.current, _currentPosition, targetPoint);
                UpdatePosition(deltaPosition);

                _speedHandler.IncreaseSpeed(Time.deltaTime);
                onMoving?.Invoke(_currentPosition);
                yield return null;
            }

            _transform.position = targetPoint;
            _speedHandler.Stop();

            callback?.Invoke();
        }

        private Vector2 _currentPosition 
            => _transform.position;

        private bool IsArrived(Vector2 targetPoint, Vector2 deltaPosition)
            => Vector2.SqrMagnitude(targetPoint - _currentPosition) <= deltaPosition.sqrMagnitude;

        private Vector2 CalculateDeltaPosition(float speed, Vector2 currentPoint, Vector2 targetPoint)
        {
            var deltaTime = Time.deltaTime;
            var distance = speed * deltaTime;
            var newPosition = Vector2.MoveTowards(currentPoint, targetPoint, distance);
            
            return newPosition - currentPoint;
        }

        private void UpdatePosition(Vector2 deltaPosition)
            => _transform.position += (Vector3)deltaPosition;
    }
}
