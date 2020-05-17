using Galcon.Level.Shipping.Generator.Pool;
using Galcon.Level.Shipping.Generator.Producer;
using UnityEngine;
using Zenject;

namespace Galcon.Level.Shipping.Generator.Installers
{
    class ShipsGeneratorInstaller : MonoInstaller<ShipsGeneratorInstaller>
    {
        [SerializeField] private GameObject _ShipPrefab;
        [SerializeField] private Transform _PoolContainer; 

        public override void InstallBindings()
        {
            Container.BindMemoryPool<Ship, ShipsPool>().WithInitialSize(2).FromComponentInNewPrefab(_ShipPrefab).UnderTransform(_PoolContainer);
            Container.Bind<IShipsPool>().To<ShipsPool>().FromResolve();

            Container.Bind<IShipsProducer>().To<ShipsProducer>().AsSingle();
        }
    }
}
