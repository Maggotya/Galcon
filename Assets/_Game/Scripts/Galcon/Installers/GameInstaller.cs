using Galcon.Level;
using Galcon.Level.ScreenView;
using UnityEngine;
using Zenject;

namespace Galcon.Installers
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        [SerializeField] private NavMeshRebaker _NavMeshRebaker;
        [SerializeField] private LevelManager _LevelManager;
        [SerializeField] private BorderedArea _BorderedArea;
        [SerializeField] private Camera _Camera;

        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(_Camera).AsSingle();
            Container.Bind<IBorderedArea>().FromInstance(_BorderedArea).AsSingle();
            Container.Bind<NavMeshRebaker>().FromInstance(_NavMeshRebaker).AsSingle();
            Container.Bind<ILevelManager>().To<LevelManager>().FromInstance(_LevelManager).AsSingle();
        }
    }
}
