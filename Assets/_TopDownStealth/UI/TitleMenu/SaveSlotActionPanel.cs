using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownStealth
{
    public class SaveSlotActionPanel : MonoBehaviour
    {
        [SerializeField]
        private Button _loadGameButton = null;

        [SerializeField]
        private Button _newGameButton = null;

        [SerializeField]
        private Button _clearSaveButton = null;

        private void Awake()
        {
            _loadGameButton.onClick.AddListener(OnLoadGameButtonClick);
            _newGameButton.onClick.AddListener(OnNewGameButtonClick);
            _clearSaveButton.onClick.AddListener(OnClearSaveButtonClick);
        }

        private void OnClearSaveButtonClick()
        {
            EventManager.Instance.Trigger(SystemEvent.ClearSave);
        }

        private void OnLoadGameButtonClick()
        {
            EventManager.Instance.Trigger(SystemEvent.LoadCurrentGame);
        }

        private void OnNewGameButtonClick()
        {
            EventManager.Instance.Trigger(SystemEvent.StartNewGame);
        }
    }
}
