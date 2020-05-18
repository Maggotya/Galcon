using System.Collections;
using System.Linq;
using Galcon.Level.Planets;
using Galcon.Level.Planets.Creation.Parameters;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Tests
{
    public class SceneTest : SceneTestFixture
    {
        private IEnumerator Install()
        {
            yield return LoadScene("Game");
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

        ////////////////////////////////////////////////

        private T Resolve<T>()
        {
            var contexts = Object.FindObjectsOfType<GameObjectContext>();
            var context = contexts.FirstOrDefault(c => c.Container.TryResolve(typeof(T)) != null);

            return context != null ? context.Container.Resolve<T>() : default;
        }
    }
}
