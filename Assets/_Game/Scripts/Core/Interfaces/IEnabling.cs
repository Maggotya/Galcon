
namespace Core.Interfaces
{
    public interface IEnabling
    {
        bool enable { get; set; }
        void Enable();
        void Disable();
    }
}
