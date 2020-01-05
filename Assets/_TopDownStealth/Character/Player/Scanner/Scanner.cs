using System.Collections;
using UnityEngine;

namespace TopDownStealth
{
    public class Scanner : MonoBehaviour
    {
        [SerializeField]
        private float _waveSpeed = 1f;

        [SerializeField]
        private float _waveMaxDistance = 10f; 

        [SerializeField]
        private float _cooldownTime = 1f;

        [SerializeField]
        private SpriteRenderer _renderer = null;

        private bool _waveActive = false;

        public float WaveDistance { get; private set; } = 0f;

        private float _lastWaveTime = 0;

        private void Awake()
        {
            _renderer.gameObject.SetActive(false);
        }

        public void Activate()
        {
            if (CanActivate())
            {
                StartCoroutine(UpdateWave());
            }
        }

        private IEnumerator UpdateWave()
        {
            WaveDistance = 0;
            _waveActive = true;
            _lastWaveTime = Time.time;

            _renderer.gameObject.SetActive(true);

            while (WaveDistance < _waveMaxDistance)
            {
                WaveDistance += _waveSpeed * Time.deltaTime;
                _renderer.transform.localScale = new Vector3(WaveDistance, WaveDistance, 0);
                yield return null;
            }

            _renderer.gameObject.SetActive(false);

            _waveActive = false;
        }

        private bool CanActivate()
        {
            if (_waveActive)
            {
                return false;
            }

            return Time.time < _cooldownTime || Time.time - _lastWaveTime > _cooldownTime;
        }
    }
}
