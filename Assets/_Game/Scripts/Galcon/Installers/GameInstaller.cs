using Core.Components.UI;
using Core.Modules;
using Galcon.Level;
using Galcon.Level.ScreenView;
using UnityEngine;
using Zenject;

namespace Galcon.Installers
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        [SerializeField] private UIManager _UiManager;
        [SerializeField] private LevelManager _LevelManager;
        [SerializeField] private BorderedArea _BorderedArea;
        [SerializeField] private Camera _Camera;

        ////////////////////////////////////////////////////////////
        
        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(_Camera).AsSingle();
            Container.Bind<IUIManager>().FromInstance(_UiManager).AsSingle();
            Container.Bind<IBorderedArea>().FromInstance(_BorderedArea).AsSingle();
            Container.Bind<ITimeScaleManager>().To<TimeScaleManager>().AsTransient();
            Container.Bind<ILevelManager>().To<LevelManager>().FromInstance(_LevelManager).AsSingle();
        }
    }
}
