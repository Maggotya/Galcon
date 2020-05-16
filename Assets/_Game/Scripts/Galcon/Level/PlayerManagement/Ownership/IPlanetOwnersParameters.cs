namespace Galcon.Level.PlayerManagement.Ownership
{
    interface IPlanetOwnersParameters
    {
        IPlanetOwnerConfig[] configs { get; }

        IPlanetOwnerConfig GetConfig(string tag);
    }
}
