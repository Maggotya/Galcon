using Core;
using Core.Extensions;
using UnityEngine;

namespace Galcon.Level.PlayerManagement.Ownership
{
    public class PlanetOwner : ValueStateCheckableMonoBehaviour, IPlanetOwner
    {
        [SerializeField] private string _OwnerTag;
        [Header("Events")]
        [SerializeField] private StringUnityEvent _OnTagChanged;

        private string _lastState { get; set; }

        public bool isClear => string.IsNullOrEmpty(_OwnerTag);

        public string ownerTag {
            get => _OwnerTag;
            set => SetTag(value);
        }

        public StringUnityEvent onTagChanged {
            get => _OnTagChanged;
            set => _OnTagChanged = value;
        }

        ///////////////////////////////////////////////

        protected override void InitState() => _lastState = _OwnerTag;
        protected override bool HasStateChanged() => _lastState != _OwnerTag;
        protected override void UpdateState() => SetTag(_OwnerTag);

        ///////////////////////////////////////////////

        public void ClearTag()
            => SetTag("");

        public void SetTag(string tag)
        {
            if (_lastState == tag)
                return;

            _lastState = tag;
            _OwnerTag = tag;
            _OnTagChanged?.Invoke(tag);

            Logging.Log(_source, $"Tag chagned to {tag}");
        }

        public bool IsOwner(string tag)
            => _OwnerTag == tag;
    }
}
