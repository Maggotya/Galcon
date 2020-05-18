using Core.Structs;
using UnityEngine;

namespace Galcon.Level.Shipping.View
{
    public class ShipView : ComponentView, IShipView
    {
        private readonly SpriteRenderer _spriteRenderer;

        ///////////////////////////////////////////////////

        public ShipView(GameObject _Model)
        {
            InitComponent(ref _spriteRenderer, _Model);
        }

        ///////////////////////////////////////////////////

        public void SetColor(Color color)
        {
            if (_spriteRenderer)
                _spriteRenderer.color = color;
        }
    }
}
