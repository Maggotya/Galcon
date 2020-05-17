using Core;
using Core.Extensions;
using Core.PoolSystem;
using Galcon.Level.Planets;
using Galcon.Level.Shipping.Model;
using Galcon.Level.Shipping.Moving;
using Galcon.Level.Shipping.View;
using UnityEngine;
using Zenject;

namespace Galcon.Level.Shipping
{
    [RequireComponent(typeof(IPoolObject))]
    class Ship : MyMonoBehaviour, IShip
    {
        private IShipModel _model;
        private IShipView _view;
        private IMovingComponent _movingComponent;

        public int population => _model.population;
        public string owner => _model.ownerTag;

        ///////////////////////////////////////////////////////////////

        [Inject]
        public void Construct(IShipModel model, IShipView view, IMovingComponent movingComponent)
        {
            _model = model;
            _view = view;

            _movingComponent = movingComponent;
            _movingComponent.onMoving += OnMoving;
        }

        private void OnMoving(Vector2 position)
        {
            // ? может вернуть null, что не тождественно false или true
            if (_model.targetPlanet?.Contains(position) != true)
                return;

            _movingComponent.Stop();
            _model.targetPlanet.AcceptShip(this);

            Destroy();
        }

        ///////////////////////////////////////////////////////////////

        public int PopulateAndReturnRemainder(string ownerTag, Color color, int inputPopulation)
        {
            _view.SetColor(color);
            _model.ownerTag = ownerTag;
            _model.population = inputPopulation;

            return inputPopulation - _model.population;
        }

        public void LaunchToPlanet(IPlanet planet)
        {
            _model.targetPlanet = planet;
            _movingComponent.MoveTo(planet.gameObject.transform.position);
        }

        public void Destroy()
        {
            if (TryGetComponent<IPoolObject>(out var poolObject) == false)
                Destroy(gameObject);

            poolObject.StoreToPool();
            Logging.Log(_source, "Destroy");
        }
    }
}
