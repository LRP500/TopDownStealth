using Sirenix.OdinInspector;
using Tools;
using Tools.Navigation;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownStealth
{
    public class TitleMenuScreen : MonoBehaviour
    {
        [SerializeField]
        [BoxGroup("Actions")]
        private Button _playButton = null;

        [SerializeField]
        [BoxGroup("Actions")]
        private Button _settingsButton = null;

        [SerializeField]
        [BoxGroup("Actions")]
        private Button _exitButton = null;

        [SerializeField]
        [BoxGroup("Panels")]
        private GameObject _subPanel = null;

        [SerializeField]
        [BoxGroup("Panels")]
        private GameObject _advancedPanel = null;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _subPanel.SetActive(false);
            _advancedPanel.SetActive(false);

            _playButton.onClick.AddListener(OnPlayButtonClick);
            _settingsButton.onClick.AddListener(OnSettingsButtonClick);
            _exitButton.onClick.AddListener(NavigationManager.QuitGame);

            EventManager.Instance.Subscribe(UIEvent.SaveSlotSelected, OnSaveSlotSelected);
        }

        private void OnSettingsButtonClick()
        {
            _subPanel.SetActive(false);
            _advancedPanel.SetActive(false);
        }

        private void OnPlayButtonClick()
        {
            _subPanel.SetActive(true);
            _advancedPanel.SetActive(false);
        }

        private void OnSaveSlotSelected(object arg)
        {
            _advancedPanel.SetActive(true);
        }
    }
}
