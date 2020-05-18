using Zenject;
using System.Collections;
using UnityEngine.TestTools;
using Galcon.Level.Shipping;
using UnityEngine;
using Galcon.Level.Shipping.Model;
using Galcon.Level.Shipping.View;
using Galcon.Level.Shipping.Moving;
using Core.ScriptableObjects;
using NUnit.Framework;
using Galcon.Level.Planets;

public class IntegrationTests : ZenjectIntegrationTestFixture
{
    private void CommonInstall()
    {
        PreInstall();

        Container.Bind<IShip>().FromMethod(CreateShip).AsTransient();
        Container.Bind<IPlanet>().FromMethod(CreatePlanet).AsTransient();

        PostInstall();
    }

    ///////////////////////////////////////////////////////////
    
    [UnityTest]
    public IEnumerator CreateShip_IsNotNull()
    {
        CommonInstall();

        var ship = Container.Resolve<IShip>();
        Assert.IsNotNull(ship);

        yield break;
    }

    [UnityTest]
    public IEnumerator CreatePlanet_IsNotNull()
    {
        CommonInstall();

        var planet = Container.Resolve<IPlanet>();
        Assert.IsNotNull(planet);

        yield break;
    }

    [UnityTest]
    public IEnumerator MoveShipToPlanet_ChangePosition()
    {
        CommonInstall();

        var planet = Container.Resolve<IPlanet>();
        var ship = Container.Resolve<IShip>();

        var startPosition = planet.gameObject.transform.position + Vector3.left * 10f;
        ship.gameObject.transform.position = startPosition;

        ship.LaunchToPlanet(planet);

        yield return new WaitForSeconds(0.3f);

        Assert.AreNotEqual(startPosition, ship.gameObject.transform.position);
    }
    ///////////////////////////////////////////////////////////

    private IShip CreateShip()
    {
        var gameObject = new GameObject("Ship");

        var model = new GameObject("Model");
        model.AddComponent<SpriteRenderer>();
        model.transform.parent = gameObject.transform;

        var shipModel = new ShipModel(5, 0, "");
        var shipView = new ShipView(model);

        var speedConfigs = new FakeSpeedConfigs();
        speedConfigs.acceleration = 0;
        speedConfigs.startSpeed = 5;
        speedConfigs.maxSpeed = 15;

        var ship = gameObject.AddComponent<Ship>();
        var movingComponent = new MovingComponent(ship, gameObject.transform, speedConfigs);

        ship.Construct(shipModel, shipView, movingComponent);

        return ship;
    }

    private IPlanet CreatePlanet()
    {
        var gameObject = new GameObject("Planet");
        var planet = gameObject.AddComponent<Planet>();

        return planet;
    }
}