using Tools.Navigation;
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

        [SerializeField]
        private SceneReference _gameplayScene = null;

        private void Awake()
        {
            _titleMenuButton.onClick.AddListener(OnTitleMenuButtonClick);
            _restartButton.onClick.AddListener(OnRestartButtonClick);
        }

        private void OnTitleMenuButtonClick()
        {
        }

        private void OnRestartButtonClick()
        {
            StartCoroutine(NavigationManager.Instance.SwitchScenes(_gameplayScene));
        }
    }
}
