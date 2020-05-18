using Galcon.Level.Parameters;
using Galcon.Level.Planets.Creation.Parameters;
using Galcon.Level.PlayerManagement.InputManagement.Parameters;
using Galcon.Level.PlayerManagement.Ownership.Parameters;
using Galcon.Level.Population.Parameters;
using Galcon.Level.Shipping.Parameters;
using UnityEngine;
using Zenject;

namespace Galcon.Level.Installers
{
    [CreateAssetMenu(fileName = "ParametersInstaller", menuName = "Installers/Parameters")]
    public class ParametersInstaller : ScriptableObjectInstaller<ParametersInstaller>
    {
        [SerializeField] private LevelParameters _Level;
        [SerializeField] private InputParameters _Input;
        [SerializeField] private PlanetOwnersParameters _Owners;
        [SerializeField] private PlanetsGeneratorParameters _PlanetsGenerator;
        [SerializeField] private PlanetOwnersVisibleParameters _OwnersVisible;
        [SerializeField] private PopulationParameters _Population;
        [SerializeField] private ShipParameters _Ship;

        public override void InstallBindings()
        {
            Container.Bind<ILevelParameters>().To<LevelParameters>().FromInstance(_Level).AsSingle();
            Container.Bind<IInputParameters>().To<InputParameters>().FromInstance(_Input).AsSingle();
            Container.Bind<IPlanetOwnersParameters>().To<PlanetOwnersParameters>().FromInstance(_Owners).AsSingle();
            Container.Bind<IPlanetsGeneratorParameters>().To<PlanetsGeneratorParameters>().FromInstance(_PlanetsGenerator).AsSingle();
            Container.Bind<IPlanetOwnersVisibleParameters>().To<PlanetOwnersVisibleParameters>().FromInstance(_OwnersVisible).AsSingle();
            Container.Bind<IPopulationParameters>().To<PopulationParameters>().FromInstance(_Population).AsSingle();
            Container.Bind<IShipParameters>().To<ShipParameters>().FromInstance(_Ship).AsSingle();

        }
    }
}
