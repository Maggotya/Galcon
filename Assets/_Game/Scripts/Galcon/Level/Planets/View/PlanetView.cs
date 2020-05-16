using Core.Structs;
using UnityEngine;

namespace Galcon.Level.Planets.View
{
    class PlanetView : ComponentView, IPlanetView
    {
        private readonly Transform _transform;
        private readonly SpriteRenderer _spriteRenderer;

        //////////////////////////////////////////////

        #region CONSTRUCTOR
        public PlanetView(GameObject model)
        {
            InitComponent(ref _transform, model);
            InitComponent(ref _spriteRenderer, model);
        }
        #endregion // CONSTRUCTOR

        //////////////////////////////////////////////

        public void SetRadius(float radius)
            => _transform.localScale = Vector3.one * radius;

        public void SetColor(Color color)
            => _spriteRenderer.color = color;

        public void SetSprite(Sprite sprite)
            => _spriteRenderer.sprite = sprite;
    }
}
