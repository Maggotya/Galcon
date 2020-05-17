using Galcon.Level.PlayerManagement.Ownership;
using Galcon.Level.PlayerManagement.Ownership.Hider;
using Galcon.Level.PlayerManagement.Ownership.Parameters;
using UnityEngine;
using Zenject;

namespace Galcon.Level.Planets.Installers
{
    class PlanetBinder : MonoInstaller<PlanetBinder>
    {
        [SerializeField] private PlanetOwner _PlanetOwner;
        [SerializeField] private AutoHiderPopulationView _PopulationHider;

        [Inject] private IPlanetOwnersVisibleParameters _parameters;

        ///////////////////////////////////////////////////////////////

        public override void InstallBindings()
        {
            _PopulationHider.SetVisibleOwners(_parameters.visibleFor);
            _PlanetOwner.onTagChanged.AddListener(_PopulationHider.UpdateVisibility);
        }
    }
}
