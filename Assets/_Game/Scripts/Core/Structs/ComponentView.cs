using UnityEngine;

namespace Core.Structs
{
    public abstract class ComponentView
    {
        protected string _source => GetType().ToString();

        protected void InitComponent<T>(ref T component, GameObject model) where T : Component
        {
            component = model?.GetComponentInChildren<T>(true);
            if (component == null)
                ThrowException($"Null {typeof(T)}!");
        }

        private void ThrowException(string message)
            => throw new System.NullReferenceException($"{GetType()}: {message}");
    }
}
