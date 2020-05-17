using Galcon.Level.Installers;
using Galcon.Level.Shipping.Generator.Pool;
using Galcon.Level.Shipping.Generator.Producer;
using UnityEngine;
using Zenject;

namespace Galcon.Level.Shipping.Generator.Installers
{
    class ShipsGeneratorInstaller : MonoInstaller<ShipsGeneratorInstaller>
    {
        [SerializeField] private Transform _PoolContainer;

        [Inject(Id = PrefabType.Ship)] private GameObject _prefab;

        public override void InstallBindings()
        {
            Container.BindMemoryPool<Ship, ShipsPool>().WithInitialSize(10)
                .FromComponentInNewPrefab(_prefab).UnderTransform(_PoolContainer);

            Container.Bind<IShipsPool>().To<ShipsPool>().FromResolve();
            Container.Bind<IShipsProducer>().To<ShipsProducer>().AsSingle();
        }
    }
}
