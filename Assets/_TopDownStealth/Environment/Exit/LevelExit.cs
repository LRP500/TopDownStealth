using System.Collections;
using Tools;
using Tools.Extensions;
using UnityEngine;

namespace TopDownStealth
{
    [RequireComponent(typeof(Collider))]
    public class LevelExit : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _fade = null;

        [SerializeField]
        private float _fadeDuration = 1f;

        private bool _triggered = false;

        private void Awake()
        {
            _fade.alpha = 0;
        }

        private void OnDisable()
        {
            _fade.alpha = 0;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_triggered)
            {
                _triggered = true;
                StartCoroutine(InitiateLevelChange());
            }
        }

        private IEnumerator InitiateLevelChange()
        {
            Time.timeScale = 0;

            float elapsed = 0;

            while (!_fade.alpha.AlmostEqual(1, 0.001f))
            {
                elapsed += Time.deltaTime;
                _fade.alpha = Mathf.Lerp(_fade.alpha, 1, elapsed / _fadeDuration);
                yield return null;
            }

            _fade.alpha = 1;

            EventManager.Instance.Trigger(GameEvent.LevelExit);
        }
    }
}