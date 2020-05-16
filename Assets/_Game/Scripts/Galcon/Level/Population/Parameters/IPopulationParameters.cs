namespace Galcon.Level.Population.Parameters
{
    interface IPopulationParameters
    {
        float proportionToEvictOnShips { get; }
        int populationPerInterval { get; }
        float updateIntervalInSeconds { get; } 
    }
}
