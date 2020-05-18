using Core.Extensions;

namespace Galcon.Level.PlayerManagement.Ownership
{
    public interface  IPlanetOwner
    {
        bool isClear { get; }
        string ownerTag { get; set; }
        StringUnityEvent onTagChanged { get; set; }

        bool IsOwner(string tag);
        void SetTag(string tag);
        void ClearTag();
    }
}
