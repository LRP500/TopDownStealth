using System.Collections;
using UnityEngine;

namespace TopDownStealth
{
    public class Detectable : MonoBehaviour
    {
        [SerializeField]
        private Material _material = null;

        public bool IsDetected { get; private set; } = false;
        public bool IsDetectable { get; private set; } = true;

        private void Awake()
        {
            SetDetected(false);
        }

        public void Detect(float duration)
        {
            StopAllCoroutines();
            StartCoroutine(UpdateDetectionState(duration));
        }

        public void SetDetectable(bool detectable)
        {
            IsDetectable = detectable;
        }

        private void SetDetected(bool detected)
        {
            IsDetected = detected;

            /// Outline shader property
            _material.SetFloat("_EnableOutline", IsDetected ? 1 : 0);

            /// Minimap shader keyword
            if (IsDetected)
            {
                Shader.EnableKeyword("MINIMAP_ENABLED");
            }
            else
            {
                Shader.DisableKeyword("MINIMAP_ENABLED");
            }
        }

        private IEnumerator UpdateDetectionState(float duration)
        {
            SetDetected(true);

            yield return new WaitForSeconds(duration);

            SetDetected(false);
        }
    }
}