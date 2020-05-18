using UnityEngine;

namespace Galcon.Level.PlayerManagement.Ownership.Parameters
{
    public interface  IPlanetOwnerConfig
    {
        string ownerTag { get; }
        Color ownerColor { get; }
    }
}
