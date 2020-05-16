using Core.Extensions;
using UnityEngine;

namespace Galcon.Level.ScreenView
{
    class Border : MyMonoBehaviour, IBorder
    {
        [SerializeField] private BorderType _Type;

        private Transform _transform;
        private IBorderDistanceCalculator _distance;

        public BorderType type => _Type;
        public Vector2 position => _transform?.position ?? transform.position;
        public Vector2 size => _transform?.localScale ?? transform.localScale;

        //////////////////////////////////////////

        private void OnEnable()
        {
            Initialize();
            Attach(ref _transform);
        }

        //////////////////////////////////////////

        public float GetDistance(Vector2 point)
            => _distance.Calculate(point);

        //////////////////////////////////////////

        private void Initialize()
        {
            if (_distance == null)
                _distance = new BorderDistanceCalculator(this);
        }
    }
}
