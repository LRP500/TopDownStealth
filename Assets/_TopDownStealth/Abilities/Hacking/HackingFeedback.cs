using Tools.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownStealth
{
    public class HackingFeedback : MonoBehaviour
    {
        [SerializeField]
        private CameraVariable _camera = null;

        [SerializeField]
        private Image _gauge = null;

        [SerializeField]
        private FloatVariable _progress = null;

        [SerializeField]
        private TransformVariable _target = null;

        private void Awake()
        {
            _progress.Subscribe(RefreshGauge);
            _gauge.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _progress.Unsubscribe(RefreshGauge);
        }

        private void RefreshGauge()
        {
            _gauge.gameObject.SetActive(_target.Value);

            if (_target.Value)
            {
                _gauge.transform.position = _camera.Value.WorldToScreenPoint(_target.Value.position);
                _gauge.fillAmount = _progress.Value;
            }
        }
    }
}
