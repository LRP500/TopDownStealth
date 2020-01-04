using System.Collections;
using Tools.Navigation;
using UnityEngine;

namespace TopDownStealth
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField]
        private GameObject _loadingText = null;

        [SerializeField]
        private GameObject _continueText = null;

        [SerializeField]
        private SceneReference _gameplayScene = null;

        private void Start()
        {
            StartCoroutine(FakeLoading());
        }

        private void OnContinueButtonClicked()
        {
            Continue();
        }

        private void Continue()
        {
            StartCoroutine(NavigationManager.Instance.FastLoad(_gameplayScene));
        }

        /// <summary>
        /// Sike.
        /// </summary>
        /// <returns></returns>
        private IEnumerator FakeLoading()
        {
            _continueText.SetActive(false);
            _loadingText.SetActive(true);

            yield return NavigationManager.Instance.UnloadAll();

            yield return new WaitForSeconds(2f);

            _loadingText.SetActive(false);
            _continueText.SetActive(true);

            StartCoroutine(WaitForInput());
        }

        private IEnumerator WaitForInput()
        {
            yield return new WaitUntil(() => Input.anyKey);

            Continue();
        }
    }
}