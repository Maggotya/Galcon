﻿using UnityEngine.Events;

namespace Core.Components.UI
{
    public interface IScreen
    {
        ScreenType type { get; }
        bool countedAsFirst { get; }
        bool countedAsLast { get; }
        bool opened { get; }

        UnityEvent onOpened { get; set; }
        UnityEvent onClosed { get; set; }

        void Open();
        void Close();
    }
}
