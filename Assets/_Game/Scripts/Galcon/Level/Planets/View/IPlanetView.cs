using UnityEngine;

namespace Galcon.Level.Planets.View
{
    public interface  IPlanetView 
    { 
        void SetRadius(float size);
        void SetColor(Color color);
        void SetSprite(Sprite sprite);
    }
}
