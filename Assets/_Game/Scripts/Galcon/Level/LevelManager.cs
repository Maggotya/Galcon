using Core.Extensions;
using Galcon.Level.InitialConfiguration;
using Galcon.Level.Planets.Manager;
using Zenject;

namespace Galcon.Level
{
    class LevelManager : MyMonoBehaviour, ILevelManager
    {
        private IPlanetsManager _planetsManager;
        private IPlanetsConfigurator _planetsConfigurator;

        ////////////////////////////////////////////////////////
        
        [Inject]
        public void Construct(IPlanetsManager planetsManager, IPlanetsConfigurator planetsConfigurator)
        {
            _planetsManager = planetsManager;
            _planetsConfigurator = planetsConfigurator;
        }

        ////////////////////////////////////////////////////////

        private void Start()
        {
            _planetsManager.GeneratePlanets();
            _planetsConfigurator.Configure(_planetsManager.planets);
        }
    }
}
