using Sirenix.OdinInspector;
using UnityEngine;

namespace TopDownStealth
{
    public class Minimap : MonoBehaviour
    {
        [SerializeField]
        private CharacterVariable _target = null;

        [SerializeField]
        private bool _followTarget = false;

        [SerializeField]
        private bool _rotateWithTarget = false;

        [SerializeField]
        private float _offsetZ = 0f;

        [SerializeField]
        private Color _overDrawColor = Color.white;

        [SerializeField]
        private Color _detectableColor = Color.white;

        private void OnValidate()
        {
            Shader.SetGlobalColor("_OverDrawColor", _overDrawColor);
            Shader.SetGlobalColor("_DetectableColor", _detectableColor);
        }

        private void LateUpdate()
        {
            if (_followTarget)
            {
                Vector3 position = _target.Value.transform.position;
                transform.position = new Vector3(position.x, transform.position.y, position.z + _offsetZ);
            }

            if (_rotateWithTarget)
            {
                transform.rotation = Quaternion.Euler(90f, _target.Value.transform.eulerAngles.y, 0f);
            }
        }
    }
}
