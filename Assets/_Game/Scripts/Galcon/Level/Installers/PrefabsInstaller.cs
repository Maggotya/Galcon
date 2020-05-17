
using UnityEngine;
using Zenject;

namespace Galcon.Level.Installers
{
    [CreateAssetMenu(fileName = "PrefabsInstaller", menuName = "Installers/Prefabs")]
    class PrefabsInstaller : ScriptableObjectInstaller<PrefabsInstaller>
    {
        [SerializeField] private GameObject _Planet;
        [SerializeField] private GameObject _Ship;

        public override void InstallBindings()
        {
            Container.Bind<GameObject>().WithId(PrefabType.Planet).FromInstance(_Planet);
            Container.Bind<GameObject>().WithId(PrefabType.Ship).FromInstance(_Ship);
        }
    }
}
