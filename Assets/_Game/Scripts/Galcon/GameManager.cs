﻿using Core;
using Core.Components.UI;
using Core.Extensions;
using Core.Modules;
using Galcon.Level;
using UnityEngine;
using Zenject;

namespace Galcon
{
    public class GameManager : MyMonoBehaviour
    {
        private ITimeScaleManager _timeScaler;
        private ILevelManager _levelManager;
        private IUIManager _uiManager;

        public bool paused { get; private set; }

        ///////////////////////////////////////////////////

        [Inject]
        public void Construct(ILevelManager levelManager, IUIManager uiManager, ITimeScaleManager timeScaler)
        {
            _levelManager = levelManager;
            _timeScaler = timeScaler;
            _uiManager = uiManager;
        }

        ///////////////////////////////////////////////////

        private void Start()
            => _uiManager?.Open(ScreenType.Start);

        ///////////////////////////////////////////////////

        [ContextMenu("Start Game")]
        public void StartGame()
        {
            _levelManager?.StartLevel();
            _uiManager?.Open(ScreenType.Game);
        }

        [ContextMenu("Pause Game")]
        public void PauseGame()
            => SetPause(true);

        [ContextMenu("Continue Game")]
        public void ContinueGame()
            => SetPause(false);

        [ContextMenu("Quit Game")]
        public void QuitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        ///////////////////////////////////////////////////

        private void SetPause(bool status)
        {
            if (paused == status)
                return;

            paused = status;
            _timeScaler?.EnableScale(!status);

            // тут я понял, что нужна история оперций, чтобы просто отменить
            // а не вызывать явно ScreenType.Game
            if (status) _uiManager?.Open(ScreenType.Pause);
            else _uiManager?.Open(ScreenType.Game);

            Logging.Log(_source, status ? "Paused" : "Unpaused");
        }
    }
}
