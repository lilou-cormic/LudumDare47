using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PurpleCable
{
    public class MainMenu : MonoBehaviour
    {
        private void Start()
        {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        public void StartGame()
        {
            StartCoroutine(GoToScene("Main"));
        }

        public void ShowCredits()
        {
            StartCoroutine(GoToScene("Credits"));
        }

        public void ShowControls()
        {
            StartCoroutine(GoToScene("Controls"));
        }

        public void ShowSettings()
        {
            StartCoroutine(GoToScene("Settings"));
        }

        public void GoToMenu()
        {
            StartCoroutine(GoToScene("Menu"));
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }

        public void LoadScene(string sceneName)
        {
            StartCoroutine(GoToScene(sceneName));
        }

        public static IEnumerator GoToScene(string sceneName)
        {
            FadeInOut.FadeOut();

            while (FadeInOut.IsFading)
                yield return null;

            SceneManager.LoadScene(sceneName);
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            FadeInOut.FadeIn();
        }
    }
}
