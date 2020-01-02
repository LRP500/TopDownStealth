using Tools;
using Tools.Navigation;
using UnityEngine;

namespace TopDownStealth
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField]
        private SceneReference _gameOverScene = null;

        [SerializeField]
        private SceneReference _gameplayScene = null;

        private void Awake()
        {
            EventManager.Instance.Subscribe(GameEvent.GameOver, OnGameOver);
            EventManager.Instance.Subscribe(GameEvent.LevelExit, OnLevelExit);
        }

        private void OnDestroy()
        {
            EventManager.Instance.Unsubscribe(GameEvent.GameOver, OnGameOver);
            EventManager.Instance.Unsubscribe(GameEvent.LevelExit, OnLevelExit);
        }

        private void OnGameOver(object arg)
        {
            StartCoroutine(NavigationManager.Instance.FastLoad(_gameOverScene));
        }

        private void OnLevelExit(object arg)
        {
            StartCoroutine(NavigationManager.Instance.DeepLoad(_gameplayScene, null));
        }
    }
}
