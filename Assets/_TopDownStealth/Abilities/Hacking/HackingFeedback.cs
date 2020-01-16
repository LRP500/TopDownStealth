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

        [SerializeField]
        private Vector2 _positionOffset = Vector2.zero;

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
                Vector3 targetPosition = _camera.Value.WorldToScreenPoint(_target.Value.position);
                Vector3 offset = new Vector3(_positionOffset.x, _positionOffset.y, 0);
                _gauge.transform.position =  targetPosition + offset;
                _gauge.fillAmount = _progress.Value;
            }
        }
    }
}
