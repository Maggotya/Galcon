using UnityEngine;

namespace Core.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SpeedConfigs", menuName = "Configs/Speed")]
    public class SpeedConfigs : ScriptableObject, ISpeedConfigs
    {
        [SerializeField] private float _StartSpeed;
        [SerializeField] private float _MaxSpeed;
        [SerializeField] private float _Acceleration;
        [SerializeField] private bool _NavMesh;

        ////////////////////////////////////////////////////

        public float startSpeed => _StartSpeed;
        public float maxSpeed => _MaxSpeed;
        public float acceleration => _Acceleration;
        public bool navMesh => _NavMesh;
    }
}
