using System.Linq;
using UnityEngine;

namespace Galcon.Level.PlayerManagement.Ownership.Hider
{
    public class AutoHiderPopulationView : MonoBehaviour
    {
        public string[] owners { get; private set; } = new string[0];

        //////////////////////////////////////////////////////

        public void SetVisibleOwners(string[] owners)
            => this.owners = owners;

        public bool IsVisible(string owner)
            => owners.Contains(owner);

        public void UpdateVisibility(string owner)
            => gameObject.SetActive(IsVisible(owner));
    }
}
