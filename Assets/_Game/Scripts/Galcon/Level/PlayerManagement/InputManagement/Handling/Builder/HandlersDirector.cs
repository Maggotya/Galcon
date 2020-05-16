using Galcon.Level.PlayerManagement.InputManagement.Parameters;

namespace Galcon.Level.PlayerManagement.InputManagement.Handling.Builder
{
    class HandlersDirector : IHandlersDirector
    {
        private readonly IHandlersBuilder _builder;
        private readonly IInputParameters _parameters;

        ///////////////////////////////////////////////////////

        public HandlersDirector(IInputParameters parameters, IHandlersBuilder builder)
        {
            _builder = builder;
            _parameters = parameters;
        }

        ///////////////////////////////////////////////////////
        
        public IInputHandler Construct()
        {
            _builder.Reset();

            if (_parameters.touch)
                _builder.AddNext<TouchInputHandler>();
            if (_parameters.mouse)
                _builder.AddNext<MouseInputHandler>();

            return _builder.Build();
        }
    }
}
