using System.Collections;
using Core.Extensions;
using Galcon.Level.InitialConfiguration;
using Galcon.Level.Planets.Manager;
using Zenject;

namespace Galcon.Level
{
    class LevelManager : MyMonoBehaviour, ILevelManager
    {
        private NavMeshRebaker _navMeshRebaker;
        private IPlanetsManager _planetsManager;
        private IPlanetsConfigurator _planetsConfigurator;

        ////////////////////////////////////////////////////////
        
        [Inject]
        public void Construct(NavMeshRebaker navMeshRebaker, IPlanetsManager planetsManager, IPlanetsConfigurator planetsConfigurator)
        {
            _navMeshRebaker = navMeshRebaker;
            _planetsManager = planetsManager;
            _planetsConfigurator = planetsConfigurator;
        }

        ////////////////////////////////////////////////////////

        private void Start()
        {
            StartLevel();
        }

        public void StartLevel()
        {
            ClearLevel();

            _planetsManager.GeneratePlanets();
            _planetsConfigurator.Configure(_planetsManager.planets);

            Invoke("BakeLevel", 0.5f);
        }

        public void ClearLevel()
        {
            _planetsManager.Clear();
        }

        private void BakeLevel()
            => _navMeshRebaker.Rebake();
    }
}
