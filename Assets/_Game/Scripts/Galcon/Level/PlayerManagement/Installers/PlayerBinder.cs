using Galcon.Level.PlayerManagement.InputManagement;
using Galcon.Level.PlayerManagement.SelectionManagement;
using UnityEngine;
using Zenject;

namespace Galcon.Level.PlayerManagement.Installers
{
    class PlayerBinder : MonoBehaviour
    {
        [Inject]
        public void Construct(IPlayer player, IInputManager inputManager, ISelectionManager selectionManager)
        {
            inputManager.onInputStationary.AddListener(selectionManager.InputStationary);
            inputManager.onInputBegan.AddListener(selectionManager.InputStarted);
            inputManager.onInputEnded.AddListener(selectionManager.InputEnded);
            inputManager.onInputMoved.AddListener(selectionManager.InputMoved);

            selectionManager.onPlanetSelected.AddListener(player.TrySelectPlanet);
            selectionManager.onPlanetDeselected.AddListener(player.TryDeselectPlanet);
            selectionManager.onPlanetClicked.AddListener(player.OnPlanetClicked);
            selectionManager.onDeselectedAll.AddListener(player.DeselectAll);
        }
    }
}
