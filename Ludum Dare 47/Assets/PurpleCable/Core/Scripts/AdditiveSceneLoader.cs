using UnityEngine;
using UnityEngine.SceneManagement;

namespace PurpleCable
{
    public class AdditiveSceneLoader : MonoBehaviour
    {
        [SerializeField]
        private string SceneName = null;

        private void Awake()
        {
            SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        }
    }
}
