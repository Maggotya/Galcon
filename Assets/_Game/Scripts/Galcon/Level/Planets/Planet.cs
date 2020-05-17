using Core;
using Core.Components;
using Core.Extensions;
using Core.Structs.Figures;
using Galcon.Level.Planets.Model;
using Galcon.Level.Planets.View;
using Galcon.Level.PlayerManagement.Ownership;
using Galcon.Level.PlayerManagement.Ownership.Parameters;
using Galcon.Level.Population;
using Galcon.Level.Shipping;
using Galcon.Level.Shipping.Manager;
using UnityEngine;
using Zenject;

namespace Galcon.Level.Planets
{
    [RequireComponent(typeof(PlanetOwner))]
    [RequireComponent(typeof(SelectableObject))]
    class Planet : MyMonoBehaviour, IPlanet
    {
        private IPlanetView _view;
        private IPlanetModel _model;
        private Circle _circle;

        private SelectableObject _selectableObject;
        private IPlanetOwnersParameters _ownersParameters;
        private IPopulationManager _populationManager;
        private IShipsManager _shipsManager;

        public IPlanetOwner owner { get; private set; }
        public Circle circle {
            get {
                _circle.point = transform.position;
                _circle.radius = _model.radius;
                return _circle;
            }
        }

        /////////////////////////////////////////////////////
        
        [Inject]
        public void Construct(IPlanetModel model, IPlanetView view, IPlanetOwner planetOwner, IPlanetOwnersParameters ownerParameters, 
            IPopulationManager populationManager, IShipsManager shipsManager, SelectableObject selectableObject)
        {
            _view = view;
            _model = model;
            _shipsManager = shipsManager;
            _circle = new Circle();
            _selectableObject = selectableObject;

            owner = planetOwner;
            _ownersParameters = ownerParameters;
            owner.onTagChanged.AddListener(OnOwnerChanged);

            _populationManager = populationManager;
            _populationManager.onPopulationExterminated.AddListener(ClearOwner);
        }

        private void OnOwnerChanged(string owner)
            => SetColor(_ownersParameters
                .GetConfig(owner).ownerColor);

        private void ClearOwner()
            => owner.ClearTag();

        /////////////////////////////////////////////////////

        #region SETTERS
        public void SetRadius(float radius)
        {
            _model.radius = radius;
            _view.SetRadius(radius);
            Logging.Log(_source, $"Set radius {radius}");
        }

        public void SetColor(Color color)
        {
            _model.color = color;
            _view.SetColor(color);
            Logging.Log(_source, $"Set color {color}");
        }

        public void SetSprite(Sprite sprite)
        {
            _view.SetSprite(sprite);
            Logging.Log(_source, "Set sprite");
        }

        public void SetSelected(bool status)
            => _selectableObject.SetSelected(status);

        public void SetPopulation(int population)
        {
            _populationManager.SetPopulation(population);
            Logging.Log(_source, $"Set population {population}");
        }
        #endregion // SETTERS

        #region SHIPPING
        public void SendShips(IPlanet targetPlanet)
        {
            var populationToSend = _populationManager.EvictPopulationForShips();
            _shipsManager.LaunchShips(owner.ownerTag, _model.color, populationToSend, targetPlanet);

            Logging.Log(_source, "Sent ships");
        }

        public void AcceptShip(IShip ship)
        {
            if (ship == null)
                return;

            var population = ship.population;

            if (owner.IsOwner(ship.owner) == false)
                population = _populationManager.AcceptOpponentsAndReturnRemainder(population);

            if (population > 0) {
                owner.SetTag(ship.owner);
                _populationManager.AcceptAlliesAndReturnRemainder(population);
            }
        }

        #endregion // SHIPPING

        #region LOCATION
        public bool Contains(Vector2 point)
            => circle.Contains(point);
        public bool Contains(Vector2 point, float eps)
        {
            var circle = this.circle;
            circle.radius += eps;
            return circle.Contains(point);
        }

        public float DistanceFromBorder(Vector2 point)
            => circle.DistanceFromBorder(point);
        public float DistanceFromCenter(Vector2 point)
            => circle.DistanceFromCenter(point);

        #endregion // LOCATION
    }
}
