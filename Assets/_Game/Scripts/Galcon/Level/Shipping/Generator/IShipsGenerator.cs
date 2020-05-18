namespace Galcon.Level.Shipping.Generator
{
    public interface  IShipsGenerator
    {
        IShip Generate(); 
        bool TryGenerate(out IShip ship);
    }
}
