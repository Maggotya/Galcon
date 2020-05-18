using Galcon.Level.Shipping.Model;
using Galcon.Level.Shipping.Moving;
using Galcon.Level.Shipping.Parameters;
using Galcon.Level.Shipping.View;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Galcon.Level.Shipping.Installers
{
    public class ShipInstaller : MonoInstaller<ShipInstaller>
    {
        [SerializeField] private GameObject _ViewModel;
        [SerializeField] private NavMeshAgent _NavMeshAgent;

        [Inject] private IShipParameters _parameters;

        //////////////////////////////////////////////////////////////////
        
        public override void InstallBindings()
        {
            Container.Bind<IShipModel>().FromMethod(CreateShipModel).AsSingle();
            Container.Bind<IShipView>().FromMethod(CreateShipView).AsSingle();

            Container.Bind<IMovingComponent>().FromMethod(CreateMovingComponent).AsSingle();

            Container.Bind<IShip>().To<Ship>().FromComponentOnRoot().AsSingle();

            ConfigureNavMeshAgent(_NavMeshAgent);
        }

        //////////////////////////////////////////////////////////////////

        private void ConfigureNavMeshAgent(NavMeshAgent agent)
        {
            if (agent == null)
                return;

            agent.enabled = _parameters.speedConfigs.navMesh;
        }

        private IShipModel CreateShipModel()
            => new ShipModel(_parameters.populationCapaciy);

        private IShipView CreateShipView()
            => new ShipView(_ViewModel);

        private IMovingComponent CreateMovingComponent()
            => _NavMeshAgent != null && _parameters.speedConfigs.navMesh ? 
            new NavMeshMovingComponent(this, transform, _NavMeshAgent, _parameters.speedConfigs) as IMovingComponent :
            new MovingComponent(this, transform, _parameters.speedConfigs) as IMovingComponent;
    }
}
