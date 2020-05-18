using Galcon.Level.PlayerManagement.InputManagement.Handling;
using Galcon.Level.PlayerManagement.InputManagement.Handling.Builder;
using Galcon.Level.PlayerManagement.InputManagement.Parameters;
using Zenject;

namespace Galcon.Level.PlayerManagement.InputManagement.Installers
{
    public class InputInstaller : MonoInstaller<InputInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputHandler>().FromMethod(CreateInputHandler).AsSingle();
        }

        private IInputHandler CreateInputHandler()
        {
            var builder = new HandlersBuilder();
            var director = new HandlersDirector(Container.Resolve<IInputParameters>(), builder);

            return director.Construct();
        }
    }
}
