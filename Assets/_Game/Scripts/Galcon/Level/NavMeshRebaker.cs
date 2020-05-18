using Core;
using Core.Extensions;
using UnityEngine;
using UnityEngine.AI;

namespace Galcon.Level
{
    public class NavMeshRebaker : MyMonoBehaviour
    {
        [ContextMenu("Rebake")]
        public void Rebake()
        {
            if (TryGetComponent<NavMeshSurface2d>(out var nvSurface) == false) {
                Logging.Error(_source, "Can't find attached NavMeshSurface2d!");
                return;
            }

            nvSurface.BuildNavMesh();
            Logging.Log(_source, "Build NavMesh");
        }
    }
}
