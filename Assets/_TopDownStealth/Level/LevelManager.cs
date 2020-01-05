using System.Collections;
using Tools;
using Tools.Extensions;
using Tools.Navigation;
using TopDownStealth.Characters;
using UnityEngine;

namespace TopDownStealth
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private Player _playerPrefab = null;

        [Space]

        [SerializeField]
        private SceneReference _gameOverScene = null;

        [Space]

        [SerializeField]
        private CanvasGroup _fadeOverlay = null;

        [SerializeField]
        private float _fadeInDuration = 1f;

        [SerializeField]
        private float _fadeOutDuration = 1f;

        [Space]

        [SerializeField]
        private CameraController _camera = null;

        private Level _level = null;

        private Player _player = null;

        private void Start()
        {
            StartCoroutine(StartLevel());
        }

        private IEnumerator Fade(float from, float to, float duration)
        {
            Time.timeScale = 0;
            float elapsed = 0;

            _fadeOverlay.alpha = from;
            _fadeOverlay.gameObject.SetActive(true);

            while (!_fadeOverlay.alpha.AlmostEqual(to, 0.01f))
            {
                elapsed += Time.deltaTime;
                _fadeOverlay.alpha = Mathf.Lerp(_fadeOverlay.alpha, to, elapsed / duration);
                yield return null;
            }

            _fadeOverlay.alpha = to;
            Time.timeScale = 1;
        }

        private IEnumerator StartLevel()
        {
            _level = Instantiate(GameManager.CurrentLevel);

            _player = Instantiate(_playerPrefab, _level.Start);
            _player.transform.parent.SetParent(null);

            _camera.SetFollowTarget(_player.transform);

            yield return Fade(1, 0, _fadeInDuration);

            EventManager.Instance?.Subscribe(GameplayEvent.GameOver, OnGameOver);
            EventManager.Instance?.Subscribe(GameplayEvent.ExitReached, OnExitReached);
        }

        private IEnumerator EndLevel()
        {
            EventManager.Instance?.Unsubscribe(GameplayEvent.GameOver, OnGameOver);
            EventManager.Instance?.Unsubscribe(GameplayEvent.ExitReached, OnExitReached);

            yield return Fade(0, 1, _fadeOutDuration);

            EventManager.Instance.Trigger(SystemEvent.LoadNextLevel);
        }

        private void OnGameOver(object arg)
        {
            StartCoroutine(NavigationManager.Instance.FastLoad(_gameOverScene));
        }

        private void OnExitReached(object arg)
        {
            StartCoroutine(EndLevel());
        }
    }
}