using System;
using Core.Extensions;
using UnityEngine.Events;

namespace Core.PoolSystem
{
    public class PoolObject : MyMonoBehaviour, IPoolObject
    {
        private UnityAction _storingToPoolAction { get; set; }
        private UnityAction _restoringFromPoolAction { get; set; }

        ////////////////////////////////////////

        public void SetStoringToPoolAction(UnityAction action)
            => _storingToPoolAction = action;

        public void SetRestoringFromPoolAction(UnityAction action)
            => _restoringFromPoolAction = action;

        public void RestoreFromPool()
        {
            gameObject.SetActive(true);
            _restoringFromPoolAction?.Invoke();
        }

        public void StoreToPool()
        {
            gameObject.SetActive(false);
            _storingToPoolAction?.Invoke();
        }
    }
}