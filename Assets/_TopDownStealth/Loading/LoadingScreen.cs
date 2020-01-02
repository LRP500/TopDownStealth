using System.Collections;
using Tools.Navigation;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownStealth
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField]
        private Button _continueButton = null;

        [SerializeField]
        private SceneReference _gameplayScene = null;

        private IEnumerator Start()
        {
            yield return StartCoroutine(NavigationManager.Instance.UnloadAllScenes());
            _continueButton.onClick.AddListener(OnContinueButtonClicked);
        }

        private void OnContinueButtonClicked()
        {
            StartCoroutine(NavigationManager.Instance.FastLoad(_gameplayScene));
        }
    }
}