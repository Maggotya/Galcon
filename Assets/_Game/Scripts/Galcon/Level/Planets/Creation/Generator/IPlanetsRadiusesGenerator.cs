namespace Galcon.Level.Planets.Creation.Generator
{
    interface IPlanetsRadiusesGenerator
    {
        bool TryGenerateRadius(float distanceToNearestPlanet, out float size);
    }
}
