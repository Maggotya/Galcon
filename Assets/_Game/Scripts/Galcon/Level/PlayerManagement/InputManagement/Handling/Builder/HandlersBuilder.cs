namespace Galcon.Level.PlayerManagement.InputManagement.Handling.Builder
{
    public class HandlersBuilder : IHandlersBuilder
    {
        private IInputHandler _handler { get; set; }

        /////////////////////////////////////////////////////

        public HandlersBuilder()
            => Reset();

        /////////////////////////////////////////////////////

        public void Reset()
            => _handler = new EmptyHandler();

        public void AddNext<T>() where T : IInputHandler
        {
            var handler = CreateHandler<T>();
            _handler.successor = handler;
        }

        public void AddPrev<T>() where T : IInputHandler
        {
            var handler = CreateHandler<T>();
            handler.successor = _handler;
            _handler = handler;
        }

        public IInputHandler Build()
            => _handler;

        /////////////////////////////////////////////////////

        private IInputHandler CreateHandler<T>() where T : IInputHandler
        {
            if (typeof(T) == typeof(MouseInputHandler))
                return new MouseInputHandler();

            if (typeof(T) == typeof(TouchInputHandler))
                return new TouchInputHandler();

            return new EmptyHandler();
        }
    }
}
