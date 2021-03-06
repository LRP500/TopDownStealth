﻿using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownStealth
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField]
        private Button _titleMenuButton = null;

        [SerializeField]
        private Button _restartButton = null;

        private void Awake()
        {
            _titleMenuButton.onClick.AddListener(OnTitleMenuButtonClick);
            _restartButton.onClick.AddListener(OnRestartButtonClick);
        }

        private void OnTitleMenuButtonClick()
        {
            EventManager.Instance.Trigger(SystemEvent.ReturnToTitleMenu);
        }

        private void OnRestartButtonClick()
        {
            EventManager.Instance.Trigger(SystemEvent.StartNewGame);
        }
    }
}
