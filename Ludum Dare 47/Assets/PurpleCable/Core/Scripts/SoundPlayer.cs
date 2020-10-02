using UnityEngine;

namespace PurpleCable
{
    /// <summary>
    /// Sound player [MonoBehaviour] - Requires AudioSource
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoBehaviour
    {
        #region Instance

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static SoundPlayer Instance { get; private set; }

        #endregion

        #region Components

        /// <summary>
        /// Audio source
        /// </summary>
        private AudioSource _audioSource;

        #endregion

        #region Properties

        /// <summary>
        /// Volume
        /// </summary>
        public static float Volume { get => Instance?._audioSource?.volume ?? 1; set => Instance._audioSource.volume = value; }

        #endregion

        #region Unity callbacks

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            // Get saved sfx volume
            _audioSource.volume = PlayerPrefs.GetFloat("SoundVolume", _audioSource.volume);

            #region Singleton
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);
            #endregion
        }

        #endregion

        #region Methods

        /// <summary>
        /// Plays the audio clip though the audio source
        /// </summary>
        /// <param name="clip">The audio clip (can be null)</param>
        /// <param name="pitch">The change in pitch (can be null or from 1 to 10) [null by default]</param>
        public static void Play(AudioClip clip, int? pitch = null)
        {
            if (clip != null && Instance?._audioSource != null)
            {
                if (pitch.HasValue)
                    Instance._audioSource.pitch = 1 + (pitch.Value / 10f);
                else
                    Instance._audioSource.pitch = 1;

                Instance?._audioSource?.PlayOneShot(clip);
            }
        }

        #endregion
    }
}
