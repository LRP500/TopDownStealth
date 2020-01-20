using System.Collections;
using TopDownStealth.Characters;
using UnityEngine;

namespace TopDownStealth
{
    public class Scanner : CooldownAbility
    {
        [SerializeField]
        private float _waveSpeed = 1f;

        [SerializeField]
        private float _waveMaxDistance = 10f;

        [SerializeField]
        private float _detectionDuration = 2f;

        [SerializeField]
        private SpriteRenderer _renderer = null;

        [SerializeField]
        private CharacterListVariable _detectableCharacters = null;

        private bool _waveActive = false;

        public float WaveDistance { get; private set; } = 0f;

        protected override void Awake()
        {
            base.Awake();

            _renderer.gameObject.SetActive(false);
        }

        protected override IEnumerator RefreshBehaviourOverTime()
        {
            WaveDistance = 0;
            _waveActive = true;
            _renderer.gameObject.SetActive(true);

            while (WaveDistance < _waveMaxDistance)
            {
                /// Update distance
                WaveDistance += _waveSpeed * Time.deltaTime;
                
                /// Update renderer
                _renderer.transform.localScale = new Vector3(WaveDistance, WaveDistance, 0);

                /// Handle object detection
                Detect();

                yield return null;
            }

            _renderer.gameObject.SetActive(false);

            _waveActive = false;
        }

        private void Detect()
        {
            foreach (Character character in _detectableCharacters.Items)
            {
                if (Vector3.Distance(transform.position, character.transform.position) <= WaveDistance)
                {
                    character.Detectable.Detect(_detectionDuration);
                }
            }
        }
    }
}