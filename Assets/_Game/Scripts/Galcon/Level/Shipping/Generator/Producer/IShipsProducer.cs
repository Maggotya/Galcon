
namespace Galcon.Level.Shipping.Generator.Producer
{
    interface IShipsProducer
    {
        bool TryProduce(out IShip ship);
        IShip Produce();
        void Collect(IShip ship);
    }
}
