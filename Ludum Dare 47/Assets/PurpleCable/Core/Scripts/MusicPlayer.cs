using UnityEngine;
using UnityEngine.SceneManagement;

namespace PurpleCable
{
    public class MusicPlayer : MonoBehaviour
    {
        public static MusicPlayer Instance { get; private set; }

        [SerializeField]
        private AudioSource MenuMusicAudioSource = null;

        [SerializeField]
        private AudioSource GameMusicAudioSource = null;

        public static float Volume
        {
            get => Instance?.MenuMusicAudioSource?.volume ?? 0.2f;

            set
            {
                if (Instance?.MenuMusicAudioSource != null)
                {
                    Instance.MenuMusicAudioSource.volume = value;
                    Instance.GameMusicAudioSource.volume = value;
                }

                MusicTransition.SetVolume();
            }
        }

        private void Start()
        {
            MenuMusicAudioSource.volume = PlayerPrefs.GetFloat("MusicVolume", MenuMusicAudioSource.volume);
            GameMusicAudioSource.volume = PlayerPrefs.GetFloat("MusicVolume", MenuMusicAudioSource.volume);

            if (Instance != null)
            {
                Instance.PlayMusic();

                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            MusicTransition.SetVolume();

            Instance.PlayMusic();
        }

        public void PlayMusic()
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Menu":
                case "Credits":
                case "About":
                case "Controls":
                case "Options":
                case "Settings":
                case "Tutorial":
                    GameMusicAudioSource.Stop();

                    if (!MenuMusicAudioSource.isPlaying)
                        MenuMusicAudioSource.Play();
                    break;

                case "Main":
                case "GameOver":
                case "Win":
                default:
                    MenuMusicAudioSource.Stop();

                    if (!GameMusicAudioSource.isPlaying)
                        GameMusicAudioSource.Play();
                    break;
            }
        }
    }
}
