using Core;
using Core.Extensions;
using Galcon.Level.PlayerManagement.InputManagement.Handling;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Galcon.Level.PlayerManagement.InputManagement
{
    public class InputManager : MyMonoBehaviour, IInputManager
    {
        [SerializeField] private Vector2UnityEvent _OnInputBegan;
        [SerializeField] private Vector2UnityEvent _OnInputMoved;
        [SerializeField] private Vector2UnityEvent _OnInputStationary;
        [SerializeField] private Vector2UnityEvent _OnInputEnded;

        private IInputHandler _inputHandler { get; set; }

        public Vector2UnityEvent onInputBegan {
            get => _OnInputBegan;
            set => _OnInputBegan = value;
        }
        public Vector2UnityEvent onInputMoved {
            get => _OnInputMoved;
            set => _OnInputMoved = value;
        }
        public Vector2UnityEvent onInputStationary {
            get => _OnInputStationary;
            set => _OnInputStationary = value;
        }
        public Vector2UnityEvent onInputEnded {
            get => _OnInputEnded;
            set => _OnInputEnded = value;
        }
        //////////////////////////////////////

        [Inject]
        public void Construct(IInputHandler handler)
        {
            if (handler == null) {
                Logging.Error(_source, "Null InputHandler!");
                return;
            }

            _inputHandler = handler;
            _inputHandler.onInputBegan += vec => _OnInputBegan?.Invoke(vec);
            _inputHandler.onInputMoved += vec => _OnInputMoved?.Invoke(vec);
            _inputHandler.onInputStationary += vec => _OnInputStationary?.Invoke(vec);
            _inputHandler.onInputEnded += vec => _OnInputEnded?.Invoke(vec);
        }

        //////////////////////////////////////

        private void Update()
        {
            if (EventSystem.current?.IsPointerOverGameObject() == true || EventSystem.current?.currentSelectedGameObject != null)
                return;

            _inputHandler?.Handle();
        }
    }
}
