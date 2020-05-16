using System.Linq;
using UnityEngine;

namespace Core.Modules.Dispenser
{
    public class RandomDispenser<T> : IDispenser<T>
    {
        private T[] _setOfDispenseredObjects;

        //////////////////////////////////////////////////////
        
        public RandomDispenser(T[] setOfDispenseredObjects)
            => _setOfDispenseredObjects = setOfDispenseredObjects ?? new T[0];

        //////////////////////////////////////////////////////

        public T Dispense()
            => _setOfDispenseredObjects.Any() ? 
            _setOfDispenseredObjects[Random.Range(0, _setOfDispenseredObjects.Length)] : 
            default;
    }
}
