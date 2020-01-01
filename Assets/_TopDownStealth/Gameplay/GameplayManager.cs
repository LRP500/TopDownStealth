using Tools;
using Tools.Navigation;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TopDownStealth
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField]
        private SceneReference _gameOverScene = null;

        private void Awake()
        {
            EventManager.Instance.Subscribe(GameEvent.GameOver, OnGameOver);
        }

        private void OnGameOver(object arg)
        {
            SceneManager.LoadSceneAsync(_gameOverScene.path);
        }
    }
}
