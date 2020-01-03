using Tools;
using Tools.Navigation;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownStealth
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        private Button _restartButton = null;

        [SerializeField]
        private Button _titleMenuButton = null;

        [SerializeField]
        private Button _exitButton = null;

        private void Awake()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClick);
            _titleMenuButton.onClick.AddListener(OnTitleMenuButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
        }

        private void OnExitButtonClick()
        {
            NavigationManager.QuitGame();
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
