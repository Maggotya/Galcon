namespace Core.Interfaces
{
    public interface IBuildDirector<T>
    {
        T Construct();
    }
}
