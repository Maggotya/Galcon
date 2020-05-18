namespace Galcon.Level.PlayerManagement.Ownership.Parameters
{
    public interface  IPlanetOwnersParameters
    {
        IPlanetOwnerConfig[] owners { get; }

        IPlanetOwnerConfig GetConfig(string tag);
    }
}
