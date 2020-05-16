using UnityEngine;
using UnityEngine.Events;

namespace Galcon.Level.PlayerManagement.InputManagement.Handling
{
    abstract class InputHandler : IInputHandler
    {
        private IInputHandler _successor { get; set; }

        public IInputHandler successor {
            get => _successor;
            set => SetSuccessor(value);
        }

        public UnityAction<Vector2> onInputBegan { get; set; }
        public UnityAction<Vector2> onInputMoved { get; set; }
        public UnityAction<Vector2> onInputStationary { get; set; }
        public UnityAction<Vector2> onInputEnded { get; set; }

        /////////////////////////////////////////////////////////

        public InputHandler() { }

        public InputHandler(IInputHandler successor)
            => SetSuccessor(successor);

        /////////////////////////////////////////////////////////

        public void Handle()
        {
            if (_canHandle == false) {
                successor?.Handle();
                return;
            }

            OnHandle();
        }

        /////////////////////////////////////////////////////////

        #region SUCCESSOR_SETTING

        private void SetSuccessor(IInputHandler successor)
        {
            UnsubscribeSuccessor(_successor);
            SubscribeSuccessor(successor);
            _successor = successor;
        }

        private void SubscribeSuccessor(IInputHandler successor)
        {
            if (successor == null)
                return;

            successor.onInputBegan += OnInputBeganInvoke;
            successor.onInputEnded += OnInputEndedInvoke;
            successor.onInputMoved += OnInputMovedInvoke;
            successor.onInputStationary += OnInputStationaryInvoke;
        }

        private void UnsubscribeSuccessor(IInputHandler successor)
        {
            if (successor == null)
                return;

            successor.onInputBegan -= OnInputBeganInvoke;
            successor.onInputEnded -= OnInputEndedInvoke;
            successor.onInputMoved -= OnInputMovedInvoke;
            successor.onInputStationary -= OnInputStationaryInvoke;
        }

        // если производить "подписки" объявлениями делегатов, то при
        // Subscribe и Unsubscribe будут выдаваться разные делегаты, 
        // и отписаться потом не получится. А функция имеет едиснтвенный инстанс.
        private void OnInputBeganInvoke(Vector2 vec) => onInputBegan?.Invoke(vec);
        private void OnInputEndedInvoke(Vector2 vec) => onInputEnded?.Invoke(vec);
        private void OnInputMovedInvoke(Vector2 vec) => onInputMoved?.Invoke(vec);
        private void OnInputStationaryInvoke(Vector2 vec) => onInputStationary?.Invoke(vec);

        #endregion // SUCCESSOR_SETTING

        #region ABSTRACT_AREA

        protected abstract bool _canHandle { get; }
        protected abstract void OnHandle();

        #endregion // ABSTRACT_AREA
    }
}
