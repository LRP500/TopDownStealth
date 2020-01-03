using Tools;
using Tools.Navigation;
using TopDownStealth;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private SceneReference _startingScene = null;

    [SerializeField]
    private SceneReference _gameplayScene = null;

    private void Awake()
    {
        Initialize();
        LoadStartingScene();
    }

    private void Initialize()
    {
        EventManager.Instance.Subscribe(SystemEvent.StartNewGame, StartNewGame);
        EventManager.Instance.Subscribe(SystemEvent.LoadCurrentGame, LoadCurrentGame);
        EventManager.Instance.Subscribe(SystemEvent.ClearSave, ClearSave);
    }

    private void LoadStartingScene()
    {
        StartCoroutine(NavigationManager.Instance.LoadAdditive(_startingScene));
    }

    private void ClearSave(object arg)
    {
    }

    private void LoadCurrentGame(object arg)
    {
    }

    private void StartNewGame(object arg)
    {
        StartCoroutine(NavigationManager.Instance.FastLoad(_gameplayScene));
    }
}
