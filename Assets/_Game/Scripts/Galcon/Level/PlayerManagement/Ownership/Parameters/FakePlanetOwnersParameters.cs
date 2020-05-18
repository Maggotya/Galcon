using System.Linq;

namespace Galcon.Level.PlayerManagement.Ownership.Parameters
{
    public class FakePlanetOwnersParameters : IPlanetOwnersParameters
    {
        public IPlanetOwnerConfig[] owners { get; set; } = new IPlanetOwnerConfig[0];

        public IPlanetOwnerConfig GetConfig(string tag)
            => owners.FirstOrDefault(o => o.ownerTag == tag);
    }
}
