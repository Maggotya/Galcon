namespace Galcon.Level.Planets.Creation.Generator
{
    public interface  IPlanetsRadiusesGenerator
    {
        bool TryGenerateRadius(float distanceToNearestPlanet, out float size);
    }
}
