using UnityEngine.Events;

namespace Core.Components.UI.Views
{
    interface IView
    {
        UnityAction onPlayOpenStarted { get; set; }
        UnityAction onPlayOpenFinished { get; set; }
        UnityAction onPlayCloseStarted { get; set; }
        UnityAction onPlayCloseFinished { get; set; }

        void PlayOpen();
        void PlayClose();
    }
}
