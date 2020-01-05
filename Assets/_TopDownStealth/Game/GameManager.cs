using Sirenix.OdinInspector;
using Tools;
using Tools.Navigation;
using TopDownStealth;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    [BoxGroup("Systems")]
    private TimeController _timeController = null;

    [SerializeField]
    [BoxGroup("Scenes")]
    private SceneReference _titleMenuScene = null;

    [SerializeField]
    [BoxGroup("Scenes")]
    private SceneReference _gameplayScene = null;

    [SerializeField]
    [BoxGroup("Scenes")]
    private SceneReference _pauseMenu = null;

    [SerializeField]
    [BoxGroup("Gameplay")]
    private LevelSelectableList _levels = null;

    public static Level CurrentLevel { get; private set; } = null;

    private void Awake()
    {
        Initialize();

#if !UNITY_EDITOR
        LoadStartingScene();
#endif
    }

    private void Update()
    {
        HandlePause();
    }

    private void HandlePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_timeController.IsGamePaused)
            {
                _timeController.Pause();
                StartCoroutine(NavigationManager.Instance.LoadAdditive(_pauseMenu, false));
            }
            else
            {
                StartCoroutine(NavigationManager.Instance.UnloadSingle(_pauseMenu));
                _timeController.Resume();
            }
        }
    }

    private void Initialize()
    {
        EventManager.Instance.Subscribe(SystemEvent.StartNewGame, StartNewGame);
        EventManager.Instance.Subscribe(SystemEvent.LoadCurrentGame, LoadCurrentGame);
        EventManager.Instance.Subscribe(SystemEvent.ClearSave, ClearSave);
        EventManager.Instance.Subscribe(SystemEvent.LoadNextLevel, LoadNextLevel);
        EventManager.Instance.Subscribe(SystemEvent.ReturnToTitleMenu, ReturnToTitleMenu);

        CurrentLevel = GetNextLevel();
    }

    private void ReturnToTitleMenu(object arg)
    {
        StartCoroutine(NavigationManager.Instance.FastLoad(_titleMenuScene));
    }

    private void LoadStartingScene()
    {
        StartCoroutine(NavigationManager.Instance.LoadAdditive(_titleMenuScene));
    }

    private void ClearSave(object arg)
    {
    }

    private void LoadCurrentGame(object arg)
    {
    }

    private void LoadNextLevel(object arg)
    {
        CurrentLevel = GetNextLevel();
        StartNewGame();
    }

    private void StartNewGame(object arg = null)
    {
        StartCoroutine(NavigationManager.Instance.DeepLoad(_gameplayScene, null));
        _timeController.Resume();
    }

    private Level GetNextLevel()
    {
        return _levels.Random();
    }
}
