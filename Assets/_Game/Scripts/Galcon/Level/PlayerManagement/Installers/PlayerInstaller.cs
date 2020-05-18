using Galcon.Level.PlayerManagement.InputManagement;
using Galcon.Level.PlayerManagement.Ownership;
using Galcon.Level.PlayerManagement.SelectionManagement;
using Zenject;

namespace Galcon.Level.PlayerManagement.Installers
{
    public class PlayerInstaller : MonoInstaller<PlayerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IPlanetOwner>().To<PlanetOwner>().FromComponentOnRoot().AsSingle();
            Container.Bind<ISelectionManager>().To<SelectionManager>().FromComponentsInChildren().AsSingle();
            Container.Bind<IInputManager>().To<InputManager>().FromComponentInChildren().AsSingle();
            Container.Bind<IPlayer>().To<Player>().FromComponentOnRoot().AsSingle();
        }
    }
}
