namespace Galcon.Level.Shipping.Generator
{
    interface IShipsGenerator
    {
        IShip Generate(); 
        bool TryGenerate(out IShip ship);
    }
}
