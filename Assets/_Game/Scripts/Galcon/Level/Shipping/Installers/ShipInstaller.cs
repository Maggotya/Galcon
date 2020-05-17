using Core.Handlers;
using Galcon.Level.Shipping.Model;
using Galcon.Level.Shipping.Moving;
using Galcon.Level.Shipping.Parameters;
using Galcon.Level.Shipping.View;
using UnityEngine;
using Zenject;

namespace Galcon.Level.Shipping.Installers
{
    class ShipInstaller : MonoInstaller<ShipInstaller>
    {
        [SerializeField] private ShipParameters _Parameters;
        [SerializeField] private GameObject _ViewModel;

        //////////////////////////////////////////////////////////////////
        
        public override void InstallBindings()
        {
            Container.Bind<IShipParameters>().To<ShipParameters>().FromInstance(_Parameters).AsSingle();

            Container.Bind<IShipModel>().FromMethod(CreateShipModel).AsSingle();
            Container.Bind<IShipView>().FromMethod(CreateShipView).AsSingle();

            Container.Bind<IMovingComponent>().FromMethod(CreateMovingComponent).AsSingle();

            Container.Bind<IShip>().To<Ship>().FromComponentOnRoot().AsSingle();
        }

        //////////////////////////////////////////////////////////////////

        private IShipModel CreateShipModel()
            => new ShipModel(_Parameters.populationCapaciy);

        private IShipView CreateShipView()
            => new ShipView(_ViewModel);

        private IMovingComponent CreateMovingComponent()
            => new MovingComponent(this, transform, CreateSpeedHandler());

        private ISpeedHandler CreateSpeedHandler()
            => new SpeedHandler(
                _Parameters.speedConfigs.startSpeed,
                _Parameters.speedConfigs.acceleration,
                _Parameters.speedConfigs.maxSpeed);
    }
}
