namespace Galcon.Level.PlayerManagement.Ownership.Parameters
{
    interface IPlanetOwnersParameters
    {
        IPlanetOwnerConfig[] owners { get; }

        IPlanetOwnerConfig GetConfig(string tag);
    }
}
