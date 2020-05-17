using Core.Structs;
using UnityEngine;

namespace Galcon.Level.Shipping.View
{
    class ShipView : ComponentView, IShipView
    {
        private readonly SpriteRenderer _spriteRenderer;

        ///////////////////////////////////////////////////

        public ShipView(GameObject _Model)
        {
            InitComponent(ref _spriteRenderer, _Model);
        }

        ///////////////////////////////////////////////////

        public void SetColor(Color color)
            => _spriteRenderer.color = color;
    }
}
