using Core.Interfaces;

namespace Galcon.Level.PlayerManagement.InputManagement.Handling.Builder
{
    interface IHandlersBuilder : IBuilder<IInputHandler>, IResetable
    {
        void AddNext<T>() where T : IInputHandler;
        void AddPrev<T>() where T : IInputHandler;
    }
}
