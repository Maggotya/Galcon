using UnityEngine;

namespace Galcon.Level.Planets.Model
{
    struct PlanetModel : IPlanetModel
    {
        private float _radius;
        private Color _color;

        public float radius {
            get => _radius;
            set => _radius = value;
        }

        public Color color {
            get => _color;
            set => _color = value;
        }
    }
}
