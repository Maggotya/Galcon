using System.Linq;
using UnityEngine;

namespace Core.Extensions
{
    public class MyMonoBehaviour : MonoBehaviour
    {
        protected string _source => GetType().ToString();

        protected bool Attach<T>(ref T component) where T : class
        {
            AttachSafe(ref component);

            if (component == null)
                Logging.Error(_source, $"{typeof(T)} isn't attached!");

            return component != null;
        }

        protected bool AttachSafe<T>(ref T component) where T : class
        {
            if (component == null)
                component = GetComponents<T>()?.FirstOrDefault(c => c as Object != this);
            return component != null;
        }

        protected void CheckField<T>(T component)
        {
            if (component == null)
                Logging.Error(_source, $"{typeof(T)} isn't attached!");
        }
    }
}
