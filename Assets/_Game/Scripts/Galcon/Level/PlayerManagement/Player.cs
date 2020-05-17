using System.Linq;
using Core;
using Core.Extensions;
using Galcon.Level.Planets;
using Galcon.Level.PlayerManagement.Installers;
using Galcon.Level.PlayerManagement.Ownership;
using Galcon.Level.PlayerManagement.SelectionManagement;
using UnityEngine;
using Zenject;

namespace Galcon.Level.PlayerManagement
{
    [RequireComponent(typeof(IPlanetOwner))]
    [RequireComponent(typeof(PlayerBinder))]
    class Player : MyMonoBehaviour, IPlayer
    {
        private IPlanetOwner _owner { get; set; }
        private SelectedPlanets _selectedPlanets { get; set; }

        //////////////////////////////////////////////////////////////
        
        [Inject]
        public void Construct(IPlanetOwner owner)
        {
            _owner = owner;
            _selectedPlanets = CreateSelectedPlanets();


        }

        #region INITIALIZATION

        // здесь используются callback'и, т.к. внутри класса имеются проверки,
        // которые могут отклонить добавление или удаление эелемента. вносить в них
        // логику по проверке собственничества планетой мне не захотелось, как и выносить наружу
        // то, что может быть проверено внутри класса, тоже.
        // использовать bool на выходе для Add и Remove также не стал, т.к. для этого
        // бы пришлось делать всякие трюки, чтобы правильно выполнить DeselectAll. 
        // к тому же класс всё равно требует в себя OnPlanetChangedOwner - не увидел разницы
        // в том, чтобы передавать один делегат или три.
        // решение вполне спорное, но довольно быстро переделываемое
        private SelectedPlanets CreateSelectedPlanets()
            => new SelectedPlanets(
                OnPlanetSelect,
                OnPlanetDeselect,
                OnPlanetChangedOwner);

        private void OnPlanetSelect(IPlanet planet)
        {
            planet.SetSelected(true);
            Logging.Log(_source, "Selected planet");
        }

        private void OnPlanetDeselect(IPlanet planet)
        {
            planet.SetSelected(false);
            Logging.Log(_source, "Deselected planet");
        }

        private void OnPlanetChangedOwner(IPlanet planet)
        {
            if (CanHandlePlanet(planet))
                return;

            _selectedPlanets.Remove(planet);
        }
        #endregion // INITIALIZATION

        //////////////////////////////////////////////////////////////

        public void TrySelectPlanet(IPlanet planet)
        {
            if (CanHandlePlanet(planet) == false)
                return;

            _selectedPlanets.Add(planet);
        }    

        public void TryDeselectPlanet(IPlanet planet)
        {
            if (CanHandlePlanet(planet) == false)
                return;

            _selectedPlanets.Remove(planet);
        }

        public void OnPlanetClicked(IPlanet planet)
        {
            if (planet == null)
                return;

            var planets = _selectedPlanets.planets.Where(p => p != planet);

            foreach (var p in planets)
                p.SendShips(planet);

            DeselectAll();

            if (CanHandlePlanet(planet)) 
                TrySelectPlanet(planet);
        }

        public void DeselectAll()
            => _selectedPlanets.RemoveAll();

        //////////////////////////////////////////////////////////////

        private bool CanHandlePlanet(IPlanet planet)
            => planet != null && planet.owner.IsOwner(_owner.ownerTag);
    }
}
