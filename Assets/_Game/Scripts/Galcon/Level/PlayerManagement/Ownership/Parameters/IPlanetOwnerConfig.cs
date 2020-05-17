using UnityEngine;

namespace Galcon.Level.PlayerManagement.Ownership.Parameters
{
    interface IPlanetOwnerConfig
    {
        string ownerTag { get; }
        Color ownerColor { get; }
    }
}
