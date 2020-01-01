using Tools;
using Tools.Navigation;
using UnityEngine;

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
            StartCoroutine(NavigationManager.Instance.SwitchScenes(_gameOverScene));
        }
    }
}
