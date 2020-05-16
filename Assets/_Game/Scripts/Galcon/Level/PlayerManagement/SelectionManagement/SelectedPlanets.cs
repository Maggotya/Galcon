using System.Collections.Generic;
using System.Linq;
using Galcon.Level.Planets;
using UnityEngine.Events;

namespace Galcon.Level.PlayerManagement.SelectionManagement
{
    // этот класс использует связку IPlanet и UnityAction для того, чтобы
    // при добавлении и удалении планеты в(из) множество(-а) можно было совершать
    // подписку(отписку) на указанное действия. А для этого нужно сохранить ссылку на событие.
    // если делать через чистое объявление делегатов каждый раз, то RemoveListener ожидаемо 
    // ничего не удаляет, что неизбежно приведёт к багам и утечкам памяти.
    class SelectedPlanets
    {
        private UnityAction<IPlanet> _onPlanetAdded;
        private UnityAction<IPlanet> _onPlanetRemoved;
        private UnityAction<IPlanet> _onPlanetChangedOwner;

        private HashSet<CallbackedPlanet> _selectedPlanets;

        public IPlanet[] planets => _selectedPlanets.Select(cp => cp.planet).ToArray();

        /////////////////////////////////////////////////////////////////////

        public SelectedPlanets(UnityAction<IPlanet> onPlanetAdded, UnityAction<IPlanet> onPlanetRemoved, 
            UnityAction<IPlanet> onPlanetChangedOwner)
        {
            _onPlanetAdded = onPlanetAdded;
            _onPlanetRemoved = onPlanetRemoved;
            _onPlanetChangedOwner = onPlanetChangedOwner;

            _selectedPlanets = new HashSet<CallbackedPlanet>();
        }

        /////////////////////////////////////////////////////////////////////

        public bool IsSelected(IPlanet planet)
            => _selectedPlanets.Any(sp => sp.Equals(planet));

        public void Add(IPlanet planet)
        {
            if (IsSelected(planet))
                return;

            var element = CreateElement(planet);
            element.Subscribe();

            _onPlanetAdded?.Invoke(planet);
            _selectedPlanets.Add(element);
        }

        public void Remove(IPlanet planet)
        {
            if (IsSelected(planet) == false)
                return;

            var element = GetElement(planet);
            element.Unsubscribe();

            _onPlanetRemoved(planet);
            _selectedPlanets.Remove(element);
        }

        public void RemoveAll()
        {
            foreach (var sp in _selectedPlanets.ToArray())
                Remove(sp.planet);
        }

        //////////////////////////////////////////////////////////////

        private CallbackedPlanet GetElement(IPlanet planet)
            => _selectedPlanets.FirstOrDefault(sp => sp.Equals(planet));

        private CallbackedPlanet CreateElement(IPlanet planet)
            => new CallbackedPlanet(planet, OnPlanetChangedOwner(planet));

        private UnityAction<string> OnPlanetChangedOwner(IPlanet planet)
            => tag => _onPlanetChangedOwner?.Invoke(planet);
    }
}
