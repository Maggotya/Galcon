namespace Core.Interfaces
{
    public interface ILaunchable
    {
        bool launched { get; }
        bool paused { get; }

        void Launch();
        void Pause();
        void Stop();
    }
}
