using Core;
using Core.Extensions;
using Galcon.Level.InitialConfiguration;
using Galcon.Level.Planets.Manager;
using Zenject;

namespace Galcon.Level
{
    public class LevelManager : MyMonoBehaviour, ILevelManager
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

        public void StartLevel()
        {
            ClearLevel();

            _planetsManager.GeneratePlanets();
            _planetsConfigurator.Configure(_planetsManager.planets);

            Invoke("BakeLevel", 0.5f);
            Logging.Log(_source, "Started level");
        }

        public void ClearLevel()
        {
            _planetsManager.Clear();
            Logging.Log(_source, "Cleared level");
        }

        ////////////////////////////////////////////////////////

        private void BakeLevel()
            => _navMeshRebaker.Rebake();
    }
}
