using System.Collections;
using UnityEngine;

namespace TopDownStealth
{
    public class Detectable : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer _renderer = null;

        [SerializeField]
        private float _detectionDuration = 2f;

        public bool Detected { get; private set; } = false;

        private void Awake()
        {
            SetDetected(false);
        }

        public void Detect()
        {
            StopAllCoroutines();
            StartCoroutine(UpdateDetectionState());
        }

        private void SetDetected(bool detected)
        {
            Detected = detected;
            _renderer.material.SetFloat("_EnableOutline", Detected ? 1 : 0);
        }

        private IEnumerator UpdateDetectionState()
        {
            SetDetected(true);

            yield return new WaitForSeconds(_detectionDuration);

            SetDetected(false);
        }
    }
}