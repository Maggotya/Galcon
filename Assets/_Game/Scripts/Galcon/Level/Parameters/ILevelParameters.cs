namespace Galcon.Level.Parameters
{
    public interface  ILevelParameters
    {
        string[] players { get; }
        int initialPlanetsEveryPlayerHas { get; }
        int initialPopulationOnEveryPlanet { get; }
    }
}
