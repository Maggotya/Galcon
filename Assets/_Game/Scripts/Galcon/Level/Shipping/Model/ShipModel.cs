using Galcon.Level.Planets;
using UnityEngine;

namespace Galcon.Level.Shipping.Model
{
    struct ShipModel : IShipModel
    {
        private int _maxPopulation;
        private int _population;
        private string _ownerTag;
        private IPlanet _targetPlanet;

        public int maxPopulation => _maxPopulation;
        public int population {
            get => _population;
            set => _population = Mathf.Min(value, _maxPopulation);
        }
        public string ownerTag {
            get => _ownerTag;
            set => _ownerTag = value;
        }
        public IPlanet targetPlanet {
            get => _targetPlanet;
            set => _targetPlanet = value;
        }

        ///////////////////////////////////////////////////////////////

        public ShipModel(int maxPopulation)
        {
            _maxPopulation = maxPopulation;
            _population = 0;
            _ownerTag = "";
            _targetPlanet = null;
        }

        public ShipModel(int maxPopulation, int population) : this(maxPopulation)
            => _population = population;

        public ShipModel(int maxPopulation, int population, string ownerTag) : this(maxPopulation, population)
            => _ownerTag = ownerTag;

        public ShipModel(int maxPopulation, int population, string ownerTag, IPlanet targetPlanet) : this(maxPopulation, population, ownerTag)
            => _targetPlanet = targetPlanet;
    }
}
