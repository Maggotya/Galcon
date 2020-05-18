namespace Galcon.Level.PlayerManagement.InputManagement.Handling
{
    public class EmptyHandler : InputHandler
    {
        protected override bool _canHandle => false;
        protected override void OnHandle() { }

        ///////////////////////////////////////////////////////
        
        public EmptyHandler() : base()
        { }

        public EmptyHandler(IInputHandler successor) : base(successor)
        { }
    }
}
