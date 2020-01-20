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
        private CharacterVariable _player = null;

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
                Vector3 dir = (_player.Value.transform.position - _target.Value.position).normalized;
                Vector3 cross = Vector3.Cross(dir, Vector3.up).normalized;
                Vector3 offset = dir + (cross / 1.25f);
                Vector3 targetPos = _camera.Value.WorldToScreenPoint(_target.Value.position + offset);

                _gauge.transform.position = targetPos;
                _gauge.fillAmount = _progress.Value;
            }
        }
    }
}
