using Tools.Extensions;
using Tools.Variables;
using TopDownStealth.Characters;
using UnityEngine;

namespace TopDownStealth
{
    public class PositionMarker : MonoBehaviour
    {
        [SerializeField]
        private CameraVariable _camera = null;

        [SerializeField]
        private RectTransform _rect = null;

        [SerializeField]
        private Vector2 _borderSize = Vector2.zero;

        private Transform _origin = null;

        private Guard _target = null;

        public void Initialize(Transform origin, Guard target)
        {
            _origin = origin;
            _target = target;
        }

        private void Update()
        {
            _rect.gameObject.SetActive(_target.Detectable.IsDetected);

            if (_target.Detectable.IsDetected)
            {
                Rotate();
                Position();
            }
        }

        private void Rotate()
        {
            Transform origin = _origin.transform;
            Vector3 direction = (_target.transform.position - origin.position).normalized;
            float angle = -Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);

            //if (angle.InRange(-45, 45))
            //{
            //    angle = 0;
            //}
            //else if (angle.InRange(45, 135))
            //{
            //    angle = 90;
            //}
            //else if (angle.InRange(-135, -45))
            //{
            //    angle = -90;
            //}
            //else
            //{
            //    angle = 180;
            //}

            _rect.localEulerAngles = new Vector3(0, 0, angle);
        }

        private void Position()
        {
            Vector3 screenPos = _camera.Value.WorldToScreenPoint(_target.transform.position);
            bool visible = !(screenPos.x.InRange(0, Screen.width) && screenPos.y.InRange(0, Screen.height));
            _rect.gameObject.SetActive(visible);

            if (visible)
            {
                screenPos.x = Mathf.Clamp(screenPos.x, _borderSize.x, Screen.width - _borderSize.x);
                screenPos.y = Mathf.Clamp(screenPos.y, _borderSize.y, Screen.height - _borderSize.y);
                _rect.position = screenPos;
            }
        }
    }
}