namespace Galcon.Level.Population.Parameters
{
    public interface  IPopulationParameters
    {
        float proportionToEvictOnShips { get; }
        int populationPerInterval { get; }
        float updateIntervalInSeconds { get; } 
    }
}
