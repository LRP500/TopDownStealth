using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownStealth
{
    public class SaveSlotButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button = null;

        private void Awake()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            EventManager.Instance.Trigger(UIEvent.SaveSlotSelected);
        }
    }
}
