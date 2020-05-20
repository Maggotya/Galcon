using System.Collections;
using System.Linq;
using Galcon;
using Galcon.Level;
using Galcon.Level.Planets;
using Galcon.Level.Planets.Creation.Parameters;
using Galcon.Level.Planets.Manager;
using Galcon.Level.PlayerManagement;
using Galcon.Level.PlayerManagement.Ownership;
using Galcon.Level.PlayerManagement.SelectionManagement;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Tests
{
    public class SceneTest : SceneTestFixture
    {
        private Camera _camera;
        private GameManager _gameManager;
        private ILevelManager _levelManager;
        private IPlayer _player;
        private IPlanetOwner _planetOwner;
        private IPlanetsManager _planetManager;
        private ISelectionManager _selectionManager;

        private IEnumerator Install()
        {
            yield return LoadScene("Game");

            _camera = Camera.main;

            _gameManager = SceneContainer.Resolve<GameManager>();
            _levelManager = SceneContainer.Resolve<ILevelManager>();
            _player = _levelManager?.gameObject?.GetComponentInChildren<IPlayer>();
            _planetOwner = _player?.gameObject?.GetComponent<IPlanetOwner>();
            _selectionManager = _player?.gameObject?.GetComponentInChildren<ISelectionManager>();
            _planetManager = _levelManager?.gameObject?.GetComponentInChildren<IPlanetsManager>();

            _gameManager.StartGame();
        }

        ////////////////////////////////////////////////

        [UnityTest]
        public IEnumerator CheckCountOfPlanets_SatisfiesScriptableObjectParameters()
        {
            yield return Install();

            var generatorParameters = Resolve<IPlanetsGeneratorParameters>();
            var enemy = Object.FindObjectsOfType<Planet>();

            Assert.That(enemy.Length >= generatorParameters.minPlanetsCount);
            Assert.That(enemy.Length <= generatorParameters.maxPlanetsCount);
        }

        [UnityTest]
        public IEnumerator PlayerMokneyJob_SelectPlanetOfPlayer_PlanetIsSelected()
        {
            yield return Install();

            var planet = _planetManager.planets.FirstOrDefault(p => p.owner.IsOwner(_planetOwner.ownerTag));
            var screenCoordinatesOfPlanet = _camera.WorldToScreenPoint(planet.gameObject.transform.position);

            _selectionManager.InputStarted(screenCoordinatesOfPlanet);
            _selectionManager.InputEnded(screenCoordinatesOfPlanet);

            Assert.IsTrue(planet.isSelected);
        }

        [UnityTest]
        public IEnumerator PlayerMokneyJob_ClickOutAfterPlanetSelection_PlanetIsDeselected()
        {
            yield return Install();

            var planet = _planetManager.planets.FirstOrDefault(p => p.owner.IsOwner(_planetOwner.ownerTag));
            var screenCoordinatesOfPlanet = _camera.WorldToScreenPoint(planet.gameObject.transform.position);
            var screenCoordinateOfEmpty = new Vector2(-1000, -1000);

            _selectionManager.InputStarted(screenCoordinatesOfPlanet);
            _selectionManager.InputEnded(screenCoordinatesOfPlanet);

            _selectionManager.InputStarted(screenCoordinateOfEmpty);
            _selectionManager.InputEnded(screenCoordinateOfEmpty);

            Assert.IsFalse(planet.isSelected);
        }

        ////////////////////////////////////////////////

        private T Resolve<T>()
        {
            var contexts = Object.FindObjectsOfType<GameObjectContext>();
            var context = contexts.FirstOrDefault(c => c.Container.TryResolve(typeof(T)) != null);

            return context != null ? context.Container.Resolve<T>() : default;
        }
    }
}
