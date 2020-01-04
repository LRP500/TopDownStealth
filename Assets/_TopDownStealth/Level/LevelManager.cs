using Tools;
using Tools.Navigation;
using UnityEngine;

namespace TopDownStealth
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private SceneReference _gameOverScene = null;

        [SerializeField]
        private SceneReference _gameplayScene = null;

        [SerializeField]
        private GameObject _playerPrefab = null;

        [SerializeField]
        private Transform _levelStart = null;

        private void Awake()
        {
            InitializeLevel();

            EventManager.Instance?.Subscribe(GameEvent.GameOver, OnGameOver);
            EventManager.Instance?.Subscribe(GameEvent.ExitReached, OnExitReached);
        }

        private void Start()
        {
            Time.timeScale = 1;
        }

        private void OnDestroy()
        {
            EventManager.Instance?.Unsubscribe(GameEvent.GameOver, OnGameOver);
            EventManager.Instance?.Unsubscribe(GameEvent.ExitReached, OnExitReached);
        }

        private void InitializeLevel()
        {
            GameObject player = Instantiate(_playerPrefab, _levelStart);
            player.transform.parent.SetParent(null);
        }

        private void OnGameOver(object arg)
        {
            StartCoroutine(NavigationManager.Instance.FastLoad(_gameOverScene));
        }

        private void OnExitReached(object arg)
        {
            StartCoroutine(NavigationManager.Instance.DeepLoad(_gameplayScene, null));
        }
    }
}