
namespace Core.Extensions
{
    public class ValueStateCheckableMonoBehaviour : MyMonoBehaviour
    {
        private void OnEnable()
            => InitState();

        private void Update()
        {
            if (HasStateChanged())
                UpdateState();
        }

        protected virtual void InitState() { }
        protected virtual bool HasStateChanged() => false;
        protected virtual void UpdateState() { }
    }
}
