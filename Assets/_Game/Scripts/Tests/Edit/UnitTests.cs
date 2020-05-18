using System.Collections;
using Galcon.Level.Planets;
using Galcon.Level.Planets.Creation;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Tests
{
    public class UnitTests : ZenjectUnitTestFixture
    {

        [SetUp]
        public void CommonInstall()
        {
            var planetInstance = new GameObject("Planet").AddComponent<Planet>();
            Container.Bind<IPlanet>().FromInstance(planetInstance).AsTransient();

            Container.BindFactory<IPlanet, PlanetsFactory>().FromResolve();
            Container.Bind<IPlanetsFactory>().To<PlanetsFactory>().FromResolve();
        }

        ///////////////////////////////////////////////////////////////////

        [Test]
        public void CreateFactory_IsNotNull()
        {
            var factory = Container.Resolve<IPlanetsFactory>();

            Assert.IsNotNull(factory);
        }

        [Test]
        public void CreatePlanetByFactory_PlanetIsNotNull()
        {
            var factory = Container.Resolve<IPlanetsFactory>();
            var planet = factory.Create();

            Assert.IsNotNull(planet);
        }
    }
}
