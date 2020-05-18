

namespace Core.ScriptableObjects
{
    public class FakeSpeedConfigs : ISpeedConfigs
    {
        public float startSpeed { get; set; }
        public float maxSpeed { get; set; }
        public float acceleration { get; set; }
        public bool navMesh { get; set; }
    }
}
