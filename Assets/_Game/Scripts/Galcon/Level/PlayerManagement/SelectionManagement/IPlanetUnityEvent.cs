using System;
using Galcon.Level.Planets;
using UnityEngine.Events;

namespace Galcon.Level.PlayerManagement.SelectionManagement
{
    [Serializable]
    class IPlanetUnityEvent : UnityEvent<IPlanet>
    {
    }
}
