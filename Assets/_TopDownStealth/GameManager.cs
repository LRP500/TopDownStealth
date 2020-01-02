using Tools.Navigation;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private SceneReference _gameplayScene = null;

    private void Awake()
    {
        StartCoroutine(NavigationManager.Instance.LoadAdditiveScene(_gameplayScene));
    }
}
