using Galcon.Level.Shipping.Generator;
using Galcon.Level.Shipping.Generator.Position;
using UnityEngine;
using Zenject;

namespace Galcon.Level.Shipping.Installers
{
    public class ShipsManagerInstaller : MonoInstaller<ShipsManagerInstaller>
    {
        [SerializeField] private Transform _ShipsContainer;

        public override void InstallBindings()
        {
            Container.Bind<IShipsPositionGenerator>().To<RandomInPlanetShipsPositionGenerator>().AsSingle();

            Container.Bind<Transform>().FromInstance(_ShipsContainer).AsSingle().WhenInjectedInto<ShipsGenerator>();
            Container.Bind<IShipsGenerator>().To<ShipsGenerator>().AsSingle();
        }
    }
}
