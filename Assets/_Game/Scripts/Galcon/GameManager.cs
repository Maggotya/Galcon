using Core.Extensions;
using Galcon.Level;
using Zenject;

namespace Galcon
{
    public class GameManager : MyMonoBehaviour
    {
        private ILevelManager _levelManager;

        ///////////////////////////////////////////////////

        [Inject]
        public void Construct(ILevelManager levelManager)
            => _levelManager = levelManager;

        ///////////////////////////////////////////////////

        private void Start()
            => _levelManager.StartLevel();
    }
}
