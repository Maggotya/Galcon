namespace Galcon.Level.Parameters
{
    interface ILevelParameters
    {
        string[] players { get; }
        int initialPlanetsEveryPlayerHas { get; }
        int initialPopulationOnEveryPlanet { get; }
    }
}
