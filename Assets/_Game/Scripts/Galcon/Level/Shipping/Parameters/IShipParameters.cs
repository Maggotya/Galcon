using Core.ScriptableObjects;

namespace Galcon.Level.Shipping.Parameters
{
    interface IShipParameters
    {
        int populationCapaciy { get; }
        ISpeedConfigs speedConfigs { get; }
    }
}
