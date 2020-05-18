using Core;
using Core.Extensions;
using Galcon.Level.Planets.Manager;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Galcon.Level.PlayerManagement.SelectionManagement
{
    public class SelectionManager : MyMonoBehaviour, ISelectionManager
    {
        [SerializeField] private IPlanetUnityEvent _OnPlanetClicked;
        [SerializeField] private IPlanetUnityEvent _OnPlanetSelected;
        [SerializeField] private IPlanetUnityEvent _OnPlanetDeselected;
        [SerializeField] private UnityEvent _OnDeselectedAll;

        private Camera _camera;
        private IPlanetsManager _planetsManager;

        private Vector2 _startPoint { get; set; }
        private Vector2 _accuracyInScreenPercents => new Vector2(2, 2);
        private Vector2 _screenSize => new Vector2(Screen.width, Screen.height);

        public IPlanetUnityEvent onPlanetSelected {
            get => _OnPlanetSelected;
            set => _OnPlanetSelected = value;
        }
        public IPlanetUnityEvent onPlanetDeselected {
            get => _OnPlanetDeselected;
            set => _OnPlanetDeselected = value;
        }
        public IPlanetUnityEvent onPlanetClicked {
            get => _OnPlanetClicked;
            set => _OnPlanetClicked = value;
        }
        public UnityEvent onDeselectedAll {
            get => _OnDeselectedAll;
            set => _OnDeselectedAll = value;
        }

        ///////////////////////////////////////////////////////////////

        [Inject]
        public void Construct(IPlanetsManager planetsManager, Camera camera)
        {
            _camera = camera;
            _planetsManager = planetsManager;
        }

        ///////////////////////////////////////////////////////////////

        public void InputStarted(Vector2 screenPoint)
        {
            _startPoint = screenPoint;
            Logging.Log(_source, "Started input");
        }

        public void InputEnded(Vector2 screenPoint)
        {
            if (WasItClick(screenPoint) == false) {
                Logging.Log(_source, "Cancel input");
                return;
            }

            var point = new Vector3(screenPoint.x, screenPoint.y, _camera.transform.position.z);
            var worldPoint = _camera.ScreenToWorldPoint(point);

            var planet = _planetsManager.GetPlanetByPosition(worldPoint);

            if (planet != null) _OnPlanetClicked?.Invoke(planet);
            else _OnDeselectedAll?.Invoke();

            Logging.Log(_source, "Clicked");
        }

        public void InputStationary(Vector2 screenPoint)
        { }

        public void InputMoved(Vector2 screenPoint)
        { }

        ///////////////////////////////////////////////////////////////

        private bool WasItClick(Vector2 screenPoint)
        {
            var difference = screenPoint - _startPoint;
            var currentAccuracy = GetPercentageShare(difference, _screenSize);

            return currentAccuracy.x <= _accuracyInScreenPercents.x && 
                currentAccuracy.y <= _accuracyInScreenPercents.y;
        }

        private Vector2 GetPercentageShare(Vector2 shareValue, Vector2 wholeValue)
        {
            var result = Vector2.zero;

            for (var i = 0; i < 2; i++)
                result[i] = GetPercentageShare(shareValue[0], wholeValue[0]);

            return result;
        }

        private float GetPercentageShare(float shareValue, float wholeValue)
            => Mathf.Abs(shareValue) / Mathf.Abs(wholeValue) * 100f;
    }
}
