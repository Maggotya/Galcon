using Galcon.Level.Shipping.Generator.Position;
using Galcon.Level.Shipping.Generator.Producer;
using UnityEngine;

namespace Galcon.Level.Shipping.Generator
{
    public class ShipsGenerator : IShipsGenerator
    {
        private readonly Transform _container;
        private readonly IShipsProducer _producer;
        private readonly IShipsPositionGenerator _positionGenerator;

        /////////////////////////////////////////////////////////////////
        
        public ShipsGenerator(Transform container, IShipsProducer producer, IShipsPositionGenerator positionGenerator)
        {
            _container = container;
            _producer = producer;
            _positionGenerator = positionGenerator;
        }

        /////////////////////////////////////////////////////////////////

        public bool TryGenerate(out IShip ship)
            => ( ship = Generate() ) != null;

        public IShip Generate()
        {
            if (_producer.TryProduce(out var ship) == false)
                return null;

            ship.gameObject.transform.parent = _container;
            ship.gameObject.transform.position = _positionGenerator.GeneratePosition();

            return ship;
        }

    }
}
