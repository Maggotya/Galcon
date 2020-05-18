using System;
using Galcon.Level.Planets;
using UnityEngine.Events;

namespace Galcon.Level.PlayerManagement.SelectionManagement
{
    [Serializable]
    public class IPlanetUnityEvent : UnityEvent<IPlanet>
    {
    }
}
