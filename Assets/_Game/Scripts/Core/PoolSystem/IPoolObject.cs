using System;
using Core.Interfaces;
using UnityEngine.Events;

namespace Core.PoolSystem
{
    public interface IPoolObject : IGameObjectHost
    {
        void SetStoringToPoolAction(UnityAction action);
        void SetRestoringFromPoolAction(UnityAction action);
        void RestoreFromPool();
        void StoreToPool();
    }
}
