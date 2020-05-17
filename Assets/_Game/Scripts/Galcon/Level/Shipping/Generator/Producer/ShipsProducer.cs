using Core.PoolSystem;
using Galcon.Level.Shipping.Generator.Pool;

namespace Galcon.Level.Shipping.Generator.Producer
{
    class ShipsProducer : IShipsProducer
    {
        private readonly IShipsPool _pool;

        ////////////////////////////////////////////////////
        
        public ShipsProducer(IShipsPool pool)
        {
            _pool = pool;
        }

        ////////////////////////////////////////////////////

        public bool TryProduce(out IShip ship)
            => ( ship = Produce() ) != null;

        public IShip Produce()
        {
            var ship = _pool.Spawn();
            Subscribe(ship);

            return ship;
        }

        public void Collect(IShip ship) 
        {
            if (ship == null)
                return;

            _pool.Despawn(ship);
        }

        ////////////////////////////////////////////////////

        private void Subscribe(IShip ship)
        {
            if (ship == null)
                return;

            if (ship.gameObject.TryGetComponent<IPoolObject>(out var poolObject))
                poolObject.SetStoringToPoolAction(() => Collect(ship));
        }
    }
}
