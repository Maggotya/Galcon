using UnityEngine;

namespace Galcon.Level.PlayerManagement.Ownership
{
    interface IPlanetOwnerConfig
    {
        string ownerTag { get; }
        Color ownerColor { get; }
    }
}
