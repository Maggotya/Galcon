using System;
using Galcon.Level.Planets;
using UnityEngine.Events;

namespace Galcon.Level.PlayerManagement.SelectionManagement
{
    public struct  CallbackedPlanet : IEquatable<CallbackedPlanet>, IEquatable<IPlanet>
    {
        private IPlanet _planet;
        private UnityAction<string> _callback;

        public IPlanet planet => _planet;
        public UnityAction<string> callback => _callback;

        ///////////////////////////////////////////////////////////

        public CallbackedPlanet(IPlanet planet, UnityAction<string> callback)
        {
            _planet = planet;
            _callback = callback;
        }

        ///////////////////////////////////////////////////////////

        public void Subscribe()
            => _planet.owner.onTagChanged.AddListener(callback);
        public void Unsubscribe()
            => _planet.owner.onTagChanged.RemoveListener(callback);

        ///////////////////////////////////////////////////////////

        public override bool Equals(object obj)
        {
            if (obj is CallbackedPlanet cp)
                return Equals(cp);
            if (obj is IPlanet planet)
                return Equals(planet);

            return false;
        }
        public bool Equals(CallbackedPlanet other)
            => this.planet == other.planet;
        public bool Equals(IPlanet other)
            => this.planet == other;

        public override int GetHashCode()
            => planet?.GetHashCode() ?? 0;
    }
}
