using Core.ScriptableObjects;

namespace Galcon.Level.Shipping.Parameters
{
    public interface  IShipParameters
    {
        int populationCapaciy { get; }
        ISpeedConfigs speedConfigs { get; }
    }
}
