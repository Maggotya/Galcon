using UnityEngine;

namespace Galcon.Level.PlayerManagement.InputManagement.Parameters
{
    [CreateAssetMenu(fileName = "InputParameters", menuName = "Parameters/Input")]
    class InputParameters : ScriptableObject, IInputParameters
    {
        [SerializeField] private bool _Touch;
        [SerializeField] private bool _Mouse;

        public bool touch => _Touch;
        public bool mouse => _Mouse;
    }
}
