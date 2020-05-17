using System;
using System.Collections;
using Core.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Galcon.Level.Shipping.Moving
{
    class NavMeshMovingComponent : IMovingComponent
    {
        private readonly MonoBehaviour _host;
        private readonly NavMeshAgent _agent;
        private readonly Transform _transform;

        private Coroutine _movingCoroutine;
        private Coroutine _waitingCoroutine;
        private bool _isAgentActive => _agent && _agent.gameObject.activeInHierarchy;

        public bool moving { get; private set; }

        public UnityAction onStartMoving { get; set; }
        public UnityAction onFinishMoving { get; set; }
        public UnityAction<Vector2> onMoving { get; set; }
        public UnityAction onStopped { get; set; }

        /////////////////////////////////////////////////////////////////

        public NavMeshMovingComponent(MonoBehaviour host, Transform transform, NavMeshAgent agent, ISpeedConfigs speedConfigs)
        {
            _host = host;
            _transform = transform;

            _agent = agent;
            ConfigureAgent(ref _agent, speedConfigs);
        }

        private void ConfigureAgent(ref NavMeshAgent agent, ISpeedConfigs speedConfigs)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            agent.speed = speedConfigs.maxSpeed;
            agent.acceleration = speedConfigs.acceleration;
        }

        /////////////////////////////////////////////////////////////////

        // здесь так хитро, потому что NavMeshAgent не может быть запущен, когда GO 
        // отключен и находится вне NavMesh контейнера. А такое случается, т.к.
        // запуск и создание корабля из пула происходят в одном кадре
        public void MoveTo(Vector2 position)
        {
            StopCoroutine(ref _waitingCoroutine);
            StopCoroutine(ref _movingCoroutine);

            _waitingCoroutine = _host.StartCoroutine(Waiting(() => {
                moving = true;
                _agent.Warp(_transform.position);
                _agent.isStopped = false;
                _agent.SetDestination(position);

                _movingCoroutine = _host.StartCoroutine(MovingCoroutine(position, OnMovingFinished));
                onStartMoving?.Invoke();
                StopCoroutine(ref _waitingCoroutine);
            }));
        }

        public void Stop()
        {
            if (moving == false)
                return;

            moving = false;
            if (_isAgentActive)
                _agent.isStopped = true;

            StopCoroutine(ref _movingCoroutine);
            StopCoroutine(ref _waitingCoroutine);
            onStopped?.Invoke();
        }

        /////////////////////////////////////////////////////////////////

        #region WAITING_ACTIONS
        private IEnumerator Waiting(Action callback)
        {
            yield return new WaitUntil(() => _isAgentActive);
            callback?.Invoke();
        }
        #endregion // WAITING_ACTIONS

        #region MOVING_ACTIONS
        private void OnMovingFinished()
        {
            moving = false;
            onFinishMoving?.Invoke();
            StopCoroutine(ref _movingCoroutine);
        }

        private IEnumerator MovingCoroutine(Vector2 targetPosition, UnityAction callback)
        {
            var lastPosition = _currentPosition;
            var deltaPosition = Vector2.zero;

            while (!IsArrived(targetPosition, deltaPosition)) {
                deltaPosition = CalculateDeltaPosition(lastPosition, _currentPosition);
                lastPosition = _currentPosition;
                onMoving?.Invoke(_currentPosition);
                yield return null;
            }

            callback?.Invoke();
        }

        private Vector2 _currentPosition
            => _transform.position;

        private bool IsArrived(Vector2 targetPoint, Vector2 deltaPosition)
            => Vector2.SqrMagnitude(targetPoint - _currentPosition) <= deltaPosition.sqrMagnitude;

        private Vector2 CalculateDeltaPosition(Vector2 lastPosition, Vector2 currentPosition)
            => currentPosition - lastPosition;

        #endregion // MOVING_ACTIONS

        private void StopCoroutine(ref Coroutine coroutine)
        {
            if (coroutine == null)
                return;

            _host.StopCoroutine(coroutine);
            coroutine = null;
        }
    }
}
