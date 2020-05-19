using Core;
using Core.Extensions;
using Galcon.Level.Finishing;
using Galcon.Level.InitialConfiguration;
using Galcon.Level.Planets;
using Galcon.Level.Planets.Manager;
using Galcon.Level.PlayerManagement;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Galcon.Level
{
    public class LevelManager : MyMonoBehaviour, ILevelManager
    {
        [SerializeField] private UnityEvent _OnLevelStarted;
        [SerializeField] private UnityEvent _OnLevelFinished;

        private IPlayer _player;
        private NavMeshRebaker _navMeshRebaker;
        private IPlanetsManager _planetsManager;
        private IPlanetsConfigurator _planetsConfigurator;
        private ILevelFinishedManager _levelFinishedChecker;

        private IPlanet[] _planets => _planetsManager?.planets ?? new IPlanet[0];

        public string status { get; private set; } = "";

        public UnityEvent onLevelStarted {
            get => _OnLevelStarted;
            set => _OnLevelFinished = value;
        }
        public UnityEvent onLevelFinished {
            get => _OnLevelFinished;
            set => _OnLevelFinished = value;
        }

        ////////////////////////////////////////////////////////

        [Inject]
        public void AddPlayer(IPlayer player)
            => _player = player;

        [Inject]
        public void AddNavMeshRebaker(NavMeshRebaker navMeshRebaker)
            => _navMeshRebaker = navMeshRebaker;

        [Inject]
        public void AddPlanetsManager(IPlanetsManager planetsManager)
            => _planetsManager = planetsManager;

        [Inject]
        public void AddPlanetsConfigurator(IPlanetsConfigurator planetsConfigurator)
            => _planetsConfigurator = planetsConfigurator;

        [Inject]
        public void AddLevelFinishedChecker(ILevelFinishedManager levelFinishedChecker)
        {
            _levelFinishedChecker = levelFinishedChecker;
            _levelFinishedChecker.onLevelFinished += OnLevelFinished;
        }

        ////////////////////////////////////////////////////////

        public void StartLevel()
        {
            ClearLevel();

            _planetsManager?.GeneratePlanets();
            _planetsConfigurator?.Configure(_planetsManager.planets);
            SubscribePlanets(_planets);

            Invoke("BakeLevel", 0.2f);

            status = "In progress";
            _OnLevelStarted?.Invoke();
            Logging.Log(_source, "Started");
        }

        public void ClearLevel()
        {
            status = "";
            _planetsManager?.Clear();
            Logging.Log(_source, "Cleared");
        }

        ////////////////////////////////////////////////////////

        private void BakeLevel()
            => _navMeshRebaker?.Rebake();

        private void SubscribePlanets(IPlanet[] planets)
        {
            foreach (var planet in planets)
                planet?.owner?.onTagChanged.AddListener(OnPlanetTagChanged);
        }

        private void OnPlanetTagChanged(string newTag)
            => _levelFinishedChecker?.CheckLevelFinished(_planets);

        private void OnLevelFinished(string winner)
        {
            status = _player?.owner?.IsOwner(winner) == true ? 
                "You win" : "You lose";

            _OnLevelFinished?.Invoke();
            Logging.Log(_source, "Finished");
        }
    }
}
