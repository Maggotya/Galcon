
namespace Galcon.Level.Shipping.Generator.Producer
{
    public interface  IShipsProducer
    {
        bool TryProduce(out IShip ship);
        IShip Produce();
        void Collect(IShip ship);
    }
}
