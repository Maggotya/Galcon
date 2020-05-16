using Galcon.Level.PlayerManagement.InputManagement.Handling;
using Galcon.Level.PlayerManagement.InputManagement.Handling.Builder;
using Galcon.Level.PlayerManagement.InputManagement.Parameters;
using UnityEngine;
using Zenject;

namespace Galcon.Level.PlayerManagement.InputManagement.Installers
{
    class InputInstaller : MonoInstaller<InputInstaller>
    {
        [SerializeField] private InputParameters _Parameters;

        public override void InstallBindings()
        {
            Container.Bind<IInputParameters>().To<InputParameters>().FromInstance(_Parameters).AsSingle();
            Container.Bind<IInputHandler>().FromMethod(CreateInputHandler).AsSingle();
        }

        private IInputHandler CreateInputHandler()
        {
            var builder = new HandlersBuilder();
            var director = new HandlersDirector(_Parameters, builder);

            return director.Construct();
        }
    }
}
